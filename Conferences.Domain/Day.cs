using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Day : Entity
    {
        public DateTime Date { get; set; }
        public List<Session> Sessions { get; set; }
        public Day()
        {
            Sessions = new List<Session>();
        }

        public Day(DateTime date) : this()
        {
            Date = date.ToStartOfDay();
        }
    }
}
