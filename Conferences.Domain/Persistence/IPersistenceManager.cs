using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Conferences.Domain.Persistence
{
    public interface IPersistenceManager : IDisposable
    {
        //Nhibernate Session (only to be used if really needed)
        ISession Session { get; }

        //Connection Methods
        void OpenConnection();
        void CloseConnection();

        //Repo Methods
        T Get<T>(int id);

        T GetByProperty<T>(string property, object value) where T : Entity;
        void Save<T>(T entity);
        void Delete<T>(T entity);


        //Transactions

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        /// <summary>
        /// Commit Current Transaction and Close Connection
        /// </summary>
        void CommitAndClose();

        /// <summary>
        /// Rollback Current Transaction and Close Connection
        /// </summary>
        void RollbackAndClose();
    }
}
