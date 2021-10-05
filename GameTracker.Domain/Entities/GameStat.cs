

namespace GameTracker.Domain.Entities
{
    public class GameStat :BaseEntity
    {
       
        public int Try1 { get; set; }
        public int Try2 { get; set; }
        public string Scenario { get; set; }
        public string Prize { get; set; }
        public string PhoneNumber { get; set; }
      
       
        public  Game Game { get; set; }
        
    }
}
