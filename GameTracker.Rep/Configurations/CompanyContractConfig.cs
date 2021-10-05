using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameTracker.Domain.Entities;

namespace GameTracker.Rep.Configurations
{
    public class CompanyContractConfig : IEntityTypeConfiguration<CompanyContract>
    {
        public void Configure(EntityTypeBuilder<CompanyContract> builder)
        {
     
            builder.HasOne(x => x.Company)
                .WithOne(x => x.CompanyContract)
                .HasForeignKey<CompanyContract>(x=>x.Id);
        }
    }
}
