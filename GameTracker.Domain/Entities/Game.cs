using System;

namespace GameTracker.Domain.Entities
{
   public class Game :BaseEntity
    {        
        public DateTime TimePlayed { get; set; }
        public int? GamerId { get; set; }
        public  Gamer Gamer { get; set; }
        public  GameStat Stat { get; set; }
        public int CompanyId { get; set; }
        public  Company Company { get; set; }
    }
}
