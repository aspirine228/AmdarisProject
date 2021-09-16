using System.Collections.Generic;

namespace GameTracker.Domain.Entities
{
    public class  Gamer:User
    {

        public int GamesPlayed { get; set; }
        public int Wallet { get; set; }
        public string PhoneNumber { get; set; }
       // public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Game> Games { get; set; }

        //public int CompanyId { get; set; }
        //public ICollection<Company> Companies { get; set; }
        public Gamer CreateDeffault()
        {
            Gamer gamer = new Gamer();
           // gamer.Id = 0;
            gamer.Name = "John Doe";
            gamer.PhoneNumber = "060000001";
            return gamer;
        }

        public string ShowResults()
        {
            return $"You played:{GamesPlayed} , your wallet:{Wallet}"; 
        }
    }
}
