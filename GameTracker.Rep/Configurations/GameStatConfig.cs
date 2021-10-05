using GameTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameTracker.Rep.Configurations
{
    public class GameStatConfig : IEntityTypeConfiguration<GameStat>
    {
        public void Configure(EntityTypeBuilder<GameStat> builder)
        {
            builder.HasOne(x => x.Game)
                 .WithOne(x => x.Stat)
                 .HasForeignKey<GameStat>(x => x.Id);
        }
    }
}
