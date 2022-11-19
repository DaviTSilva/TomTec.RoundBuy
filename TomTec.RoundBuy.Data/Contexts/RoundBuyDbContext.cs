using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using TomTec.RoundBuy.Models;

namespace TomTec.RoundBuy.Data
{
    public class RoundBuyDbContext : DbContext
    {
        public DbContext Instance { get => this; }
        protected readonly IConfiguration Configuration;

        public RoundBuyDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            // construct the DB using the project TomTec.RoundBuy.API as startup
            options.UseSqlServer(Configuration.GetConnectionString("RoundBuyDB"), b => b.MigrationsAssembly("TomTec.RoundBuy.API"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            //foreach(var reationhip in modelBuilder.Model.GetEntityTypes())
            //{

            //}
            modelBuilder.Entity<User>().HasIndex(u => new {
                u.Email,
                u.UserName,
            }).IsUnique();
            modelBuilder.Entity<Claim>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<UserType>().HasIndex(ut => ut.Name).IsUnique();

            modelBuilder.Entity<UsersClaims>()
                .HasKey(uc => new { uc.UserId, uc.ClaimId });
            modelBuilder.Entity<UsersClaims>()
                .HasOne(uc => uc.User)
                .WithMany(b => b.UsersClaims)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UsersClaims>()
                .HasOne(bc => bc.Claim)
                .WithMany(c => c.UsersClaims)
                .HasForeignKey(bc => bc.ClaimId);

        }
    }
}
