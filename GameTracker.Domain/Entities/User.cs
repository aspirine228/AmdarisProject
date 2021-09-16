//using L9GenericsColl.Enums;


namespace GameTracker.Domain.Entities
{
    public abstract class User : BaseEntity
    {
        
        public string Name { get; set; }

        public string Email { get; set; }

       // public UserState State { get; set; }

      //  public GamerProfile gamerProfile { get; set; }
    }
}
