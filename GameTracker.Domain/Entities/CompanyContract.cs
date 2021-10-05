using System;

namespace GameTracker.Domain.Entities
{
    public class CompanyContract : BaseEntity
    {              
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
 
        public  Company Company{ get; set; }

    }
}
