namespace DirectDoc2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DirectDoc2.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DirectDoc2.DAL.ClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DirectDoc2.DAL.ClinicContext";
        }

        protected override void Seed(DirectDoc2.DAL.ClinicContext context)
        {
            //Populate Tariffs
            context.Tariffs.AddOrUpdate(
                t => t.ID,
                new Tariff { TariffType = "SAMA" },
                new Tariff { TariffType = "GRN" },
                new Tariff { TariffType = "BHF/NA" }
            );
            context.SaveChanges();

            //Populate Modalities
            context.Modalities.AddOrUpdate(
                m => m.ModalityID,
                new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 3.94M, TariffID = 1, NappiCode = "491919-003" },
                new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 2, NappiCode = "491919-003" },
                new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 3, NappiCode = "491919-003" }
                );
            context.SaveChanges();

            //Populate Patients
            context.Clients.AddOrUpdate(
                c => c.ID,
                new Person
                {
                    SponsorID = null,
                    Title = "Mr",
                    FirstName = "Adewale",
                    Initials = "",
                    LastName = "Odunlami",
                    DateOfBirth = Convert.ToDateTime("15-Jan-1986"),
                    IsDependant = false
                },

                new Person
                {
                    SponsorID = 1,
                    Title = "Mr",
                    FirstName = "Oreoluwa",
                    Initials = "O.",
                    LastName = "Odunlami",
                    DateOfBirth = Convert.ToDateTime("15-Jan-1986"),
                    IsDependant = true
                }
            );
            context.SaveChanges();

            //Populate Postal Address
            context.PostalAddresses.AddOrUpdate(
                p => p.PostalAddressID,
                new PostalAddress
                {
                    PersonID = 1,
                    BoxNumber = 3456,
                    City = "Windhoek"
                }
            );
            context.SaveChanges();

            //Populate Phone Numbers
            context.Phones.AddOrUpdate(
                p => p.PhoneID,
                new Phone
                {
                    PersonID = 1,
                    PhoneType = "Cell",
                    AreaCode = 240,
                    Number = 23987432
                },

                new Phone
                {
                    PersonID = 1,
                    PhoneType = "Home",
                    AreaCode = 264,
                    Number = 93888623
                }
            );
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
