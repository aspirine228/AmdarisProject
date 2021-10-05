using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameTracker.Domain.Entities;


namespace GameTracker.Rep.Configurations
{
    public class FeedBackConfig : IEntityTypeConfiguration<FeedBack>
    {
        public void Configure(EntityTypeBuilder<FeedBack> builder)
        {
            builder
                .Property(f => f.Text).HasColumnType("text");
        }
    }
}
