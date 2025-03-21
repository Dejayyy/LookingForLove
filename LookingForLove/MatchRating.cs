using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookingForLove
{
    public class MatchRating
    {
        public int MatchId { get; set; }
        public int Rating { get; set; } // 1-5??
        public DateTime RatingDate { get; set; } = DateTime.Now;
    }
}
