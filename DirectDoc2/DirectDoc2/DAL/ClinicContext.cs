﻿using DirectDoc2.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DirectDoc2.DAL
{
    public class ClinicContext : DbContext
    {
        public ClinicContext()
            : base("ClinicContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MedicalAid> MedicalAids { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Person> Clients { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new TariffConfiguration());
            modelBuilder.Configurations.Add(new ModalityConfiguration());
        }
    }

    public class TariffConfiguration : EntityTypeConfiguration<Tariff>
    {
        public TariffConfiguration()
        {
            Property(t => t.TariffType).IsRequired().HasMaxLength(500);           
        }
    }

    public class ModalityConfiguration : EntityTypeConfiguration<Modality>
    {
        public ModalityConfiguration()
        {
            Property(m => m.TariffID).IsRequired();
            
            Property(m => m.ModalityCode).IsRequired().HasMaxLength(500);
            Property(m => m.Description).IsRequired().HasMaxLength(500);
            Property(m => m.Price).IsRequired().HasColumnType("money");
            
        }
    }
}