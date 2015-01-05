using System;
using System.Linq;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Criterion;
using NHibernate.Persister.Entity;

namespace Conferences.Domain.Persistence
{
    public class NHibernatePersistenceManager : IPersistenceManager
    {
        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; private set; }

        public NHibernatePersistenceManager(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region Connections

        public void OpenConnection()
        {
            if (Session != null && Session.IsOpen) return;
            Session = _sessionFactory.OpenSession();
        }

        public void CloseConnection()
        {
            if (Session == null || !Session.IsOpen) return;

            Session.Close();
            Session.Dispose();
        }

        #endregion

        #region Transactions

        private void CreateTransaction()
        {
            OpenConnection();
            _transaction = new NHibernateTransaction(Session);
        }

        private void DisposeTransaction()
        {
            _transaction = null;
        }

        public void BeginTransaction()
        {
            if (_transaction == null) CreateTransaction();
            _transaction.BeginTransaction();
        }

        public void CommitAndClose()
        {
            CommitTransaction();
            CloseConnection();
            Session = null;
        }

        public void CommitTransaction()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            DisposeTransaction();
        }

        public void RollbackAndClose()
        {
            RollbackTransaction();
            CloseConnection();
            Session = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            DisposeTransaction();
        }


        #endregion

        #region Repo

        public T Get<T>(int id)
        {

            return Session.Get<T>(id);

        }

        public T GetByProperty<T>(string property, object value) where T : Entity
        {
            var query = Session.CreateCriteria<T>().Add(Restrictions.Eq(property, value));
            var ret = query.List<T>();
            if (ret == null) return null;

            var itm = ret.Take(1).SingleOrDefault();
            if (itm == null) return null;

            return itm;
        }

        public void Save<T>(T entity)
        {

            if (_transaction == null)
            {
                BeginTransaction();
                Session.SaveOrUpdate(entity);
                CommitTransaction();
                return;
            }

            Session.SaveOrUpdate(entity);
        }

        public void Delete<T>(T entity)
        {

            if (_transaction == null)
            {
                BeginTransaction();
                Session.Delete(entity);
                CommitTransaction();
                return;
            }

            Session.Delete(entity);
        }


        public void Dispose()
        {
            if (Session == null) return;

            Session.Close();
            Session.Dispose();
        }

       

        private Boolean IsDirtyEntity(ISession session, Object entity)
        {

            ISessionImplementor sessionImpl = session.GetSessionImplementation();
            IPersistenceContext persistenceContext = sessionImpl.PersistenceContext;
            EntityEntry oldEntry = persistenceContext.GetEntry(entity);
            String className = oldEntry.EntityName;
            IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);

            Object[] oldState = oldEntry.LoadedState;
            Object[] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
            Int32[] dirtyProps = oldState.Select((o, i) => (oldState[i] == currentState[i]) ? -1 : i).Where(x => x >= 0).ToArray();

            return (dirtyProps.Any());

        }

        #endregion

    }
}
