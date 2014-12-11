using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Session : Entity
    {
        public string Name { get; set; }
        public string Summery { get; set; }
        public string Desc { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Room Room { get; set; } 
        public User Speaker { get; set; }
        public List<User> Attendies { get; set; }

        public Session()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Room = new Room();
            Speaker = new User();
            Attendies = new List<User>();
        }

        public Session(string name, string summery, string desc, DateTime startTime, DateTime endTime, Room room = null, User speaker1 = null)
        {
            Name = name;
            Summery = summery;
            Desc = desc;
            StartTime = startTime;
            EndTime = endTime;
            Room = room;
            Speaker = speaker1;
        }


        public void AddAttenties(string )
        {
            user = 
        }


        public void AddStartTime(DateTime startTime)
        {
            startTime = startTime.ToStartOfDay();
            if (startTime < DateTime.Now) throw new Exception("Time Must Be After Now"); //Does this neeed to be added to a list to check for double booking?
        }

        public void AddRoom
    }
}
