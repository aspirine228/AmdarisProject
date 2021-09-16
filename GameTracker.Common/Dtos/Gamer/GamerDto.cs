using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Common.Dtos.Gamer
{
    public class GamerDto
    {
        public int Id { get; set; }
       
        public int GamesPlayed { get; set; }
        public int Wallet { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
