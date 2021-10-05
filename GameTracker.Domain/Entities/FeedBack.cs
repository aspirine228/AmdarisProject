using GameTracker.Domain.Auth;


namespace GameTracker.Domain.Entities
{
    public class FeedBack:BaseEntity
    {
        public string Text { get; set; }

        public float Grade { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
