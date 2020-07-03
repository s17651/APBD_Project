using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class AdvertDbContext : DbContext
    {
        public AdvertDbContext()
        {
        }

        public AdvertDbContext(DbContextOptions options)
            : base(options)
        { 
        }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Campaign> Campaings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Building>(opt =>
            {
                opt.HasKey(b => b.IdBuilding);
                opt.Property(b => b.IdBuilding)
                    .ValueGeneratedOnAdd();

                opt.Property(b => b.Street)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(b => b.City)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(b => b.Height)
                    .HasColumnType("decimal(6, 2)");
            });

            builder.Entity<Client>(opt =>
            {
                opt.HasKey(c => c.IdClient);
                opt.Property(c => c.IdClient)
                    .ValueGeneratedOnAdd();

                opt.Property(c => c.FirstName)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.LastName)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.Email)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.Phone)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.Login)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.Password)
                    .HasMaxLength(100)
                    .IsRequired();

                opt.Property(c => c.RefreshToken)
                    .HasMaxLength(500);

                opt.Property(c => c.Salt)
                    .HasMaxLength(100)
                    .IsRequired();

            });

            builder.Entity<Banner>(opt =>
            {
                opt.HasKey(b => b.IdAdvertisement);
                opt.Property(b => b.IdAdvertisement)
                    .ValueGeneratedOnAdd();

                opt.Property(b => b.Price)
                    .HasColumnType("decimal(6, 2)");

                opt.Property(b => b.Area)
                    .HasColumnType("decimal(18, 6)");
            });

            builder.Entity<Campaign>(opt => 
            {
                opt.HasKey(c => c.IdCampaign);
                opt.Property(c => c.IdCampaign)
                    .ValueGeneratedOnAdd();

                opt.Property(c => c.PricePerSquareMeter)
                    .HasColumnType("decimal(18, 6)")
                    .IsRequired();

                opt.HasOne(c => c.FromBuilding)
                    .WithMany(b => b.CampaignsFromBuilding)
                    .HasForeignKey(c => c.FromIdBuilding)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(c => c.ToBuilding)
                    .WithMany(b => b.CampaignsToBuilding)
                    .HasForeignKey(c => c.ToIdBuilding)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                opt.HasOne(c => c.Client)
                    .WithMany(cl => cl.Campaigns)
                    .HasForeignKey(c => c.IdClient);

                opt.HasMany(c => c.Banners)
                    .WithOne(b => b.Campaign)
                    .HasForeignKey(b => b.IdCampaign);
            });
        }
    }
}
