using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conferences.Domain;
using Conferences.Domain.Persistence;

namespace TestConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            var sessionFactory = NhibernateHelper.InitializeSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                // populate the database
                using (var transaction = session.BeginTransaction())
                {
                    var user1 = new User("steve", "leonard", "steve@somewhere.com", "pass");
                    var user2 = new User("dan", "evens", "dan@somewhere.com", "pass");
                    session.SaveOrUpdate(user1);
                    session.SaveOrUpdate(user2);
                    transaction.Commit();
                }

            }
            Console.WriteLine("Users:");
            using (var session = sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var users = session.CreateCriteria(typeof (User)).List<User>();

                    foreach (var user in users)
                        Console.WriteLine("{0}{1} - {2}", user.FirstName, user.Surname, user.Email);
                }
            }
            Console.ReadLine();
        }
    }
}
