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
        public bool IsSpeaker { get; set; } //Not Sure How To Check This


        public User()
        {
            FirstName = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Image = string.Empty;
            SpeakerBio = string.Empty;
            Interests = new List<Tag>();
        }

        public User(string firstName, string surname, string email, string password) : this()
        {
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Password = password;
        }

        public User(string firstName, string surname, string email, string password, string image)
            : this(firstName, surname, email, password)
        {
            Image = image;
        }

        public User(string firstName, string surname, string email, string password, string image, string speakerBio)
            : this(firstName, surname, email, password, image)
        {
            SpeakerBio = speakerBio;
        }

    }
}
