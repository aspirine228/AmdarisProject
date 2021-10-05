
using Microsoft.EntityFrameworkCore;
using GameTracker.Domain.Entities;
using GameTracker.Rep.Configurations;
using GameTracker.Domain.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameTracker.Rep
{

    public class MyGameDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public MyGameDbContext(DbContextOptions<MyGameDbContext> options) : base(options)
        {

        }
        public DbSet<News> News { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Gamer> Gamers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }       
     
       protected override void OnModelCreating(ModelBuilder model)
        {           
            model.ApplyConfiguration(new GamerConfig());
            model.ApplyConfiguration(new CompanyContractConfig());
            model.ApplyConfiguration(new GameStatConfig());
            model.ApplyConfiguration(new NewsConfig());
            model.ApplyConfiguration(new FeedBackConfig());

            base.OnModelCreating(model);
           
            ApplyIdentityMapConfiguration(model);
        }
        private void ApplyIdentityMapConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users", SchemaConstants.Auth);
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims", SchemaConstants.Auth);
            modelBuilder.Entity<UserLogin>().ToTable("UserLogins", SchemaConstants.Auth);
            modelBuilder.Entity<UserToken>().ToTable("UserRoles", SchemaConstants.Auth);
            modelBuilder.Entity<Role>().ToTable("Roles", SchemaConstants.Auth);
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaims", SchemaConstants.Auth);
            modelBuilder.Entity<UserRole>().ToTable("UserRole", SchemaConstants.Auth);
        }
    }
}
