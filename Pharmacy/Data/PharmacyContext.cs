using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Models;

namespace PharmacyApp.Data
{
    public class PharmacyContext : DbContext
    {
        public PharmacyContext (DbContextOptions<PharmacyContext> options)
            : base(options)
        {
        }
        public DbSet<PharmacyApp.Models.Manager> Managers { get; set; }
        public DbSet<PharmacyApp.Models.Medicine> Medicines { get; set; }
        public DbSet<PharmacyApp.Models.Pharmacy> Pharmacies { get; set; }
        public DbSet<PharmacyApp.Models.PharmacyMedicine> PharmacyMedicine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().ToTable("Manager");
            modelBuilder.Entity<Medicine>().ToTable("Medicine");
            modelBuilder.Entity<Pharmacy>().ToTable("Pharmacy");
            modelBuilder.Entity<PharmacyMedicine>().ToTable("PharmacyMedicine");
        }
    }
}
