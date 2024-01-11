using Microsoft.EntityFrameworkCore;
using Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping
{
    public partial class LocalDBContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Smartphone> Smartphones { get; set; }
        public DbSet<Headphone> Headphones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-PUHPL7C\\SQLEXPRESS;Database=ScrapingDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Smartphone>()
                .HasOne(s => s.Brand)
                .WithMany()
                .HasForeignKey(s => s.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Headphone>()
                .HasOne(h => h.Brand)
                .WithMany()
                .HasForeignKey(h => h.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
