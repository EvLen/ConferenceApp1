using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Conferences.Domain.Persistence
{
    public class NhibernateHelper
    {
        public static ISessionFactory SessionFactory { get; set; }
        private const string DbFile = "conferences.db";
        public static ISessionFactory InitializeSessionFactory()
        {

            var sessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(DbFile))
                .Mappings(m =>
                    m.AutoMappings.Add(AutoMap.AssemblyOf<User>(new AutomappingConfiguration())
                        .Conventions.Setup
                        (
                            x =>
                            {
                                x.AddFromAssemblyOf<AutomappingConfiguration>();
                                x.Add(AutoImport.Never());
                            }
                         )
                        .UseOverridesFromAssemblyOf<AutomappingConfiguration>())
                )
                .ExposeConfiguration(BuildSchema);

            return sessionFactory.BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            config.SetProperty("generate_statistics", "true");
            config.SetProperty("show_sql", "false");
            config.SetProperty("adonet.batch_size", "500");
            config.SetProperty("connection.release_mode", "auto");
            config.SetProperty("hbm2ddl.keywords", "none");

                // delete the existing db on each run
            if (File.Exists(DbFile))
                File.Delete(DbFile);

                // this NHibernate tool takes a configuration (with mapping info in)
                // and exports a database schema from it
            new SchemaExport(config)
                .Create(false, true);
        }
    }
}
