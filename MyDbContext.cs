using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingEntityFrameWork
{
    public class MyDbContext : DbContext
    {
        // DbContext is building class
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AssetTracking;Integrated Security=True";
        public DbSet<Asset> Assets { get; set; }
        // public DbSet<Laptop> Laptops { get; set; }
        //public DbSet<Mobile> Mobiles { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure inheritance using a discriminator column
            modelBuilder.Entity<Asset>()
                .HasDiscriminator<string>("AssetType") // Adds an 'AssetType' column
                .HasValue<Laptop>("Laptop")
                .HasValue<Mobile>("Mobile");


            // Configure decimal precision for PurchasePrice
            modelBuilder.Entity<Asset>()
                .Property(a => a.PurchasePrice)
                .HasPrecision(18, 2); // Precision of 18, scale of 2

            // Configure precision for ExchangeRate
            modelBuilder.Entity<Office>()
                .Property(o => o.ExchangeRate)
                .HasPrecision(18, 6); // Precision of 18, scale of 6

            modelBuilder.Entity<Office>()
            .HasMany(o => o.Assets)
            .WithOne(a => a.Office)
            .HasForeignKey(a => a.OfficeId);

            // Seed initial data
            modelBuilder.Entity<Office>().HasData(
                new Office { Id = 1, Name = "New York", Currency = "USD", ExchangeRate = 1.0m },
                new Office { Id = 2, Name = "London", Currency = "GBP", ExchangeRate = 0.75m },
                new Office { Id = 3, Name = "Tokyo", Currency = "JPY", ExchangeRate = 110.0m }
            );
            //  Seed data
            modelBuilder.Entity<Laptop>().HasData(
                 new Laptop { Id = 1, Name = "MacBook", ModelName = "Air", PurchasePrice = 2000, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 1 },
                 new Laptop { Id = 2, Name = "Lenovo", ModelName = "S33", PurchasePrice = 2000, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 2 },
                 new Laptop { Id = 3, Name = "Asus", ModelName = "Rog 24", PurchasePrice = 2000, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 3 }
            );

            modelBuilder.Entity<Mobile>().HasData(
                new Mobile { Id = 4, Name = "iPhone", ModelName = "15", PurchasePrice = 1200, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 1 },
                new Mobile { Id = 5, Name = "Samsung", ModelName = "Galaxy 12", PurchasePrice = 1200, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 2 },
                new Mobile { Id = 6, Name = "Nokia", ModelName = "3310", PurchasePrice = 1200, PurchaseDate = DateTime.Now.AddYears(-1), EndOfLifeDate = DateTime.Now.AddYears(2), OfficeId = 3 }
            );
        }
    }
}
