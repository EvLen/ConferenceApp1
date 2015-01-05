using System;
using Conferences.Domain;
using Conferences.Domain.Persistence;

namespace TestConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            var sessionFactory = NhibernateHelper.InitializeSessionFactory();
            var persistenceManager = new NHibernatePersistenceManager(NhibernateHelper.InitializeSessionFactory());
            var user1 = new User("steve", "leonard", "steve@somewhere.com", "pass");
            var user2 = new User("dan", "evens", "dan@somewhere.com", "pass");
            persistenceManager.Save(user1);
            persistenceManager.Save(user2);

            foreach (var user in persistenceManager.Session.CreateCriteria(typeof(User)).List<User>())
                           Console.WriteLine("{0} {1} - {2}", user.FirstName, user.Surname, user.Email);

           
            Console.ReadLine();
        }
    }
}
