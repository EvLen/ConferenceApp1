using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class Address
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }

        public Address()
        {
            FirstLine = string.Empty;
            SecondLine = string.Empty;
            City = string.Empty;
            County = string.Empty;
            PostCode = string.Empty;
            Country = string.Empty;
        }
        
        public Address(string firstLine, string secondLine, string city, string county, string postCode, string country) : this()

        {
            FirstLine = firstLine;
            SecondLine = secondLine;
            City = city;
            County = county;
            PostCode = postCode;
            Country = country;
        }


    }
}
