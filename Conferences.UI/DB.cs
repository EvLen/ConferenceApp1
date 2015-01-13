using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conferences.Domain.Persistence;

namespace Conferences.UI
{
    public static class DBHelper
    {
        private static NHibernatePersistenceManager db;

        public static NHibernatePersistenceManager DB
        {
            get { return db; }
        }

        public static void Init()
        {
            db = new NHibernatePersistenceManager(NhibernateHelper.InitializeSessionFactory());
        }
    }
}
