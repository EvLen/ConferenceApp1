using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Session : Entity
    {
        public virtual string Name { get; set; }
        public virtual string Summery { get; set; }
        public virtual string Desc { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual Room Room { get; set; } 
        public virtual User Speaker { get; set; }
        public virtual IList<User> Attendies { get; set; }
        public virtual Day Day {get; set;}


        public Session()
        {
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Room = new Room();
            Speaker = new User();
            Attendies = new List<User>();
        }

        public Session(string name, string summery, string desc, DateTime startTime, DateTime endTime, Room room = null, User speaker1 = null):this()
        {
            Name = name;
            Summery = summery;
            Desc = desc;
            StartTime = startTime;
            EndTime = endTime;
            Room = room;
            Speaker = speaker1;
        }

        
        public virtual void Attend(User user)
        {
            if (Attendies.Contains(user)) return;
            if (Room != null && Room.Capacity <= Attendies.Count) throw new Exception("");
            // if userr attending another session
            Attendies.Add(user);
        }

        public virtual void Unattend(User user)
        {
            if (!Attendies.Contains(user)) return;
            Attendies.Remove(user);
        }

        public virtual void AssignRoom(Room room)
        {
         if (room == null) throw new Exception("bla");
         if (room.Sessions.Any(x => (x.StartTime <= EndTime && StartTime <= x.EndTime))) throw new Exception("over lap");
         Room = room;
        }

        public virtual void SetSpeaker(User speaker)
        {
            Speaker = speaker;
            // Set up speaker clash
        }

        public virtual void UpdateDetails(string name, string summery, string desc, DateTime startTime, DateTime endTime)
        {
            Name = name;
            Summery = summery;
            Desc = desc;
            StartTime = startTime;
            EndTime = endTime;
        }

    }
}
