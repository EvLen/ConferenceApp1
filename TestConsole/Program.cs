using System;
using Conferences.Domain;
using Conferences.Domain.Persistence;

namespace TestConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            //Connecting to the database
            //var sessionFactory = NhibernateHelper.InitializeSessionFactory();
            var persistenceManager = new NHibernatePersistenceManager(NhibernateHelper.InitializeSessionFactory());

            //Creating some users
            var user1 = new User("steve", "leonard", "steve@somewhere.com", "pass");
            var user2 = new User("dan", "evens", "dan@somewhere.com", "pass");
            persistenceManager.Save(user1);//saves use to database
            persistenceManager.Save(user2);

            int lastid = 0;
            //get a list of all users and display or edit them
            foreach (var user in persistenceManager.Session.CreateCriteria(typeof (User)).List<User>())
            {
                //display the users in the console
                Console.WriteLine("{0} {1} - {2}", user.FirstName, user.Surname, user.Email);
                
                // here i can edit a user e.g. 
                user.IsSpeaker = true;
                persistenceManager.Save(user);//save
                lastid = user.Id;
            }

            //if i know a users ID i can load one up and edit it and save it again
            var usr = persistenceManager.Get<User>(lastid);
            usr.SpeakerBio = "bla bla bla i like code....";
            persistenceManager.Save(usr);

            var session1 = persistenceManager.Get<Session>(1);
            session1.Attend(usr);
            persistenceManager.Save(session1);

            Console.WriteLine("Session 1 has " + session1.Attendies.Count + " users atending");


            Console.ReadLine();
        }
    }
}
