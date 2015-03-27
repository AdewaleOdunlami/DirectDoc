using DirectDoc2.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
        public DbSet<Consultation> Consultations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new TariffConfiguration());
            modelBuilder.Configurations.Add(new ModalityConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
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

            Property(m => m.NappiCode).IsRequired().HasMaxLength(500);
            Property(m => m.ModalityCode).IsRequired().HasMaxLength(500);
            Property(m => m.Description).IsRequired().HasMaxLength(500);
            Property(m => m.Price).IsRequired().HasColumnType("money");
        }
    }

    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(p => p.FirstName).IsRequired().HasMaxLength(500);
            Property(p => p.Title).IsRequired();
            Property(p => p.LastName).IsRequired().HasMaxLength(500);
        }
    }
}