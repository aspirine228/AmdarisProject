using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Domain.Entities
{
   public class Game :BaseEntity
    {
        
        public DateTime TimePlayed { get; set; }
        public int? GamerId { get; set; }
        public virtual Gamer Gamer { get; set; }

        public virtual GameStat Stat { get; set; }
    }
}
