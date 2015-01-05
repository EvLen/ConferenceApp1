using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace Conferences.Domain.Persistence
{
    public class NHibernateTransaction : ITransaction
    {
        private readonly ISession Session;
        private NHibernate.ITransaction Trans;

        public NHibernateTransaction(ISession session)
        {
            Session = session;
            Trans = Session.BeginTransaction();
        }

        public void Dispose()
        {
            Trans.Dispose();
            Dispose();
        }

        public void Commit()
        {
            Trans.Commit();
            Session.Flush();
        }

        public void Rollback()
        {
            Trans.Rollback();
        }

        public void BeginTransaction()
        {
            Trans = Session.BeginTransaction();
        }
    }
}
