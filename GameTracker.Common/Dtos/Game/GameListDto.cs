using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker.Common.Dtos.Game
{
    public class GameListDto
    {
        public int Id { get; set; }
        public DateTime TimePlayed { get; set; }
        public string phoneNumber { get; set; }
        public int Try1 { get; set; }
        public int Try2 { get; set; }
        public string Scenario { get; set; }
        public string prize { get; set; }
    }
}
