using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
            SetUpUniqueValues(modelBuilder);
            SetUpManyToManyRelationships(modelBuilder);
            SetUpOnDeleteMethod(modelBuilder);
            SetUpInitialValues(modelBuilder);
        }

        #region SetUp Methods
        private static void SetUpUniqueValues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<Claim>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<UserType>().HasIndex(ut => ut.Name).IsUnique();
            modelBuilder.Entity<PaymentMethod>().HasIndex(ut => ut.Name).IsUnique();
            modelBuilder.Entity<OfficialIdentificationType>().HasIndex(oi => oi.Name).IsUnique();
        }

        private static void SetUpManyToManyRelationships(ModelBuilder modelBuilder)
        {
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
                .HasKey(op => new { op.Id, op.OrderId, op.ProductId });
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op2 => op2.OrderId);
            modelBuilder.Entity<OrderProducts>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op2 => op2.ProductId);
        }

        private static void SetUpOnDeleteMethod(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.AlternativeAddress)
                .WithMany()
                .HasForeignKey(a2 => a2.AlternativeAddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.AdvertiserUser)
                .WithMany()
                .HasForeignKey(a2 => a2.AdvertiserUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.BuyerUser)
                .WithMany()
                .HasForeignKey(o2 => o2.BuyerUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AuthorUser)
                .WithMany()
                .HasForeignKey(c2 => c2.AuthorUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.AuthorUser)
                .WithMany()
                .HasForeignKey(r2 => r2.AuthorUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private static void SetUpInitialValues(ModelBuilder modelBuilder)
        {
            modelBuilder
            .Entity<OfficialIdentificationType>()
            .HasData(Enum.GetValues(typeof(OffcialIdentificationTypeEnum))
                .Cast<OffcialIdentificationTypeEnum>()
                .Select(e => new OfficialIdentificationType
                {
                    Id = (short)e,
                    Name = e.ToString(),
                    CreationDate = DateTime.UtcNow,
                })
            );

            modelBuilder
            .Entity<UserType>()
            .HasData(Enum.GetValues(typeof(UserTypeEnum))
                .Cast<UserTypeEnum>()
                .Select(e => new UserType
                {
                    Id = (short)e,
                    Name = e.ToString(),
                    CreationDate = DateTime.UtcNow,
                })
            );

            modelBuilder
            .Entity<PaymentMethod>()
            .HasData(Enum.GetValues(typeof(PaymentMethodEnum))
                .Cast<PaymentMethodEnum>()
                .Select(e => new PaymentMethod
                {
                    Id = (short)e,
                    Name = e.ToString(),
                    CreationDate = DateTime.UtcNow,
                })
            );
        }
        #endregion

        #region DbSets
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
        public DbSet<Category> Categories { get; set; }
        #endregion
    }
}
