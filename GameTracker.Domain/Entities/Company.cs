
using System.Collections.Generic;


namespace GameTracker.Domain.Entities
{
    public class Company :BaseEntity
    {
        public string CompanyName { get; set; }

        public  CompanyContract CompanyContract { get; set; }

        public  ICollection<Game> Games { get; set; }
    }
}
