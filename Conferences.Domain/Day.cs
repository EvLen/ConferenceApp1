﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Day : Entity
    {
        public virtual DateTime Date { get; set; }
        public virtual List<Session> Sessions { get; set; }
        public virtual Conference Conference { get; set; }
        
        public Day()
        {
            Sessions = new List<Session>();
        }

        public Day(Conference conference, DateTime date) : this()
        {
            Date = date.ToStartOfDay();
            Conference = conference;
        }

        public virtual void AddSession(Session session)
        {
            if (Sessions.Contains(session)) return;
            Sessions.Add(session);
        }

    }
}
