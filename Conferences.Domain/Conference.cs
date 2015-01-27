using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Conference : Entity
    {
        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual decimal Price { get; set; }
        public virtual IList<Day> Days { get; set; }
        public virtual IList<Room> Rooms { get; set; }

        public Conference()
        {
            Days = new List<Day>();
            Rooms = new List<Room>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Name = string.Empty;
        }

        public Conference(string name, DateTime startDate, DateTime endDate) : this()
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
        public Conference(string name, DateTime startDate, DateTime endDate, decimal price) : this(name, startDate,endDate)
        {
            Price = price;
        }

        public virtual void UpdateBasicInfo(string name, DateTime startDate, DateTime endDate) 
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public virtual void SetPice(decimal price)
        {
            if (price < 0m) throw new Exception("Failed to set price. Price cannot be a negative value");
            Price = price;
        }

        public virtual void AddDay(DateTime date)
        {
            date = date.ToStartOfDay();
            if (date < DateTime.Now) throw new Exception("Date Must Not Be In The Past");
            if (Days.SingleOrDefault(x => x.Date == date) != null) throw new Exception("Date Already Exists");
            Days.Add(new Day(this,date));
        }

        public virtual void AddRoom(string name, int capcity)
        {
            if (Rooms.Any(x => x.Name == name)) throw new Exception("ddd");
            Rooms.Add(new Room(this,name,capcity));
        }

           public virtual List<DateTime> GetAvailableDates()
        {
            var dates = new List<DateTime>();
            var dt = StartDate;
               do
               {
                   dates.Add(dt);
                   dt = dt.AddDays(1);
               }while(dt < EndDate);
               return dates;
        }

           public virtual bool HasDay(DateTime date)
           {
               return Days.Any(x => x.Date.ToStartOfDay() == date.ToStartOfDay());
           }
    }
}
