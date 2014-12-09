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
        
        
    }
}
