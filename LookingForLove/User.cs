using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class User
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string Bio { get; set; } = "No bio yet.";
        public string Interests { get; set; } = "No interests added.";
    }
}

