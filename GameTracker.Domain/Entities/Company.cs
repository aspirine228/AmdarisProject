using System;
using System.Collections.Generic;
using System.Text;

namespace GameTracker.Domain.Entities
{
    public class Company :BaseEntity
    {
        public string CompanyName { get; set; }

        public int GamerId { get; set; }
        public ICollection<Gamer> Gamers { get; set; }
    }
}
