
namespace GameTracker.Domain.Entities
{
    public abstract class Users : BaseEntity
    {
        
        public string Name { get; set; }

        public string Email { get; set; }

    }
}
