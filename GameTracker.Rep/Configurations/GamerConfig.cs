using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameTracker.Domain.Entities;

namespace GameTracker.Rep.Configurations
{
    public class GamerConfig : IEntityTypeConfiguration<Gamer>
    {
        public void Configure(EntityTypeBuilder<Gamer> builder)
        {
            builder
                .HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
