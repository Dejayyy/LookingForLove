using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class User
    {
        public string Bio { get; set; }
        public string Interests { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WhatsApp { get; set; }
        public bool IsPaidMember { get; set; } = false;
        public string Gender { get; set; }
        public string Nickname { get; set; }
        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string PossessedSkills { get; set; } = "";
        public string DesiredSkills { get; set; } = "";
        public string PreferredContactMethod { get; set; } = "Email";

    }
}

