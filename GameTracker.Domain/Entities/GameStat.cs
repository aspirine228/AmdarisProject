using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameTracker.Domain.Entities
{
    public class GameStat :BaseEntity
    {
        [ForeignKey("Game")]
      
        public int Try1 { get; set; }
        public int Try2 { get; set; }
        public string Scenario { get; set; }
        public string Prize { get; set; }
        
       
        //public int GameId
        //{ get; set; }
        public virtual Game Game { get; set; }
        
    }
}
