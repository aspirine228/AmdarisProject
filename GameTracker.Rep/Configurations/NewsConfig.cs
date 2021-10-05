using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameTracker.Domain.Entities;

namespace GameTracker.Rep.Configurations
{
    public class NewsConfig : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder
                .Property(f => f.Text).HasColumnType("text");
        }
    }
}
