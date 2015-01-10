using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conferences.Domain.Persistence;

namespace Conferences.Domain
{
    public class User : Entity
    {
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
     
        public virtual string Password { get; set; }
        public virtual string Image { get; set; }
        public virtual string SpeakerBio { get; set; }
        public virtual Address Address { get; set; }
        public virtual IList<Tag> Interests { get; set; }
        public virtual bool IsSpeaker { get; set; } //Not Sure How To Check This



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


        public virtual void UpdateDetails(string firstName, string surname, string email, string password, string image, string speakerBio)
        {
            FirstName = firstName;
            Surname = surname;
            Email = email;
            Password = password;
            Image = image;
            SpeakerBio = speakerBio;
        }

        public virtual void AddInterest(Tag interest)
        {
            if (Interests.Contains(interest)) return;
            Interests.Add(interest);
        }

    }
}
