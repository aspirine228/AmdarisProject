
using Microsoft.EntityFrameworkCore;
using GameTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameTracker.Rep.Configurations;

namespace GameTracker.Rep
{

    public class MyGameDbContext : DbContext
    {
        public MyGameDbContext(DbContextOptions<MyGameDbContext> options) : base(options)
        {

        }
        
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}

        public DbSet<Gamer> Gamers { get; set; }

       
     
       protected override void OnModelCreating(ModelBuilder model)
        {
            
            model.ApplyConfiguration(new GamerConfig());
            base.OnModelCreating(model);
            // model.Entity<Device>().Property(x => x.Name).HasColumnName("DeviceName");
        }
    }
}
