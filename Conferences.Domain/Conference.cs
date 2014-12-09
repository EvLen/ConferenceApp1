using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Conference : Entity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public IList<Day> Days { get; set; }

        public Conference()
        {
            Days = new List<Day>();
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

        public void SetPice(decimal price)
        {
            if (price < 0m) throw new Exception("Failed to set price. Price cannot be a negative value");
            Price = price;
        }

        public void AddDay(DateTime date)
        {
            if (date < DateTime.Now) throw new Exception("Date Must Not Be In The Past");
            if (Days.SingleOrDefault(x => x.Date == date) != null) throw new Exception("Date Already Exists");
            Days.Add(new Day(date));
        }
    }
}
