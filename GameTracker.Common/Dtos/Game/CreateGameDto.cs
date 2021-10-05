using System;


namespace GameTracker.Common.Dtos.Game
{
    public class CreateGameDto
    {

        public int? GamerId { get; set; }
        public int CompanyId { get; set; }
        public DateTime TimePlayed { get; set; }
        public string PhoneNumber { get; set; }
        public int Try1 { get; set; }
        public int Try2 { get; set; }
        public string Scenario { get; set; }
        public string Prize { get; set; }
    }
}
