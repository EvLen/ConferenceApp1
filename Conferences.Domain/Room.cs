using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<Session> Sessions { get; set; }
        public Conference Conference { get; set; }


        public Room()
        {
            Name = string.Empty;
            Sessions = new List<Session>();
        }

        public Room(Conference conference, string name, int capacity) : this()
        {
            Conference = conference;
            Name = name;
            Capacity = capacity;
        }


        public void UpdateDetails(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            //check sessions attendance aftyer capacity change
            foreach (var s in Sessions)
            {
                if (s.Attendies.Count > Capacity)
                {
                    //fake email....
                }
            }
        }

        
    }

}

