using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Common.Dtos.Game
{
    public class GameDto
    {
        public int Id { get; set; }
        public DateTime TimePlayed { get; set; }
        public int? GamerId { get; set; }

    }
}
