using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conferences.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string SpeakerBio { get; set; }
        public Address Address { get; set; }
        public List<Tag> Interests { get; set; }
        public bool IsSpeaker { get; set; }
    }
}
