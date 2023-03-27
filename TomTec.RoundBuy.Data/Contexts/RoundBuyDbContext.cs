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
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
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

            modelBuilder.Entity<OrderProducts>()
                .HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op2 => op2.OrderId);
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op2 => op2.ProductId);
        }

        //Auth
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UsersClaims> UsersClaims { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<OfficialIdentificationType> OfficialIdentificationTypes { get; set; }

        //Sales
        public DbSet<Product> Products { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ProductPack> ProductPacks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
