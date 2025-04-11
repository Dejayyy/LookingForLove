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
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public List<string> PossessedSkills { get; set; } = new List<string>();
        public List<string> DesiredSkills { get; set; } = new List<string>();
        public string PreferredContactMethod { get; set; } = "Email";

    }
}

