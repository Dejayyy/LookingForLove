using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class Match
    {
        public int Id { get; set; }
        public int RequestorId { get; set; }
        public int MatchedUserId { get; set; }
        public double MatchScore { get; set; }
        public DateTime MatchDate { get; set; } = DateTime.Now;
    }
}
