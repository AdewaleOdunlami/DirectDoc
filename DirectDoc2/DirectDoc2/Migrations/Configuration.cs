namespace DirectDoc2.Migrations
{
    using DirectDoc2.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DirectDoc2.DAL.ClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DirectDoc2.DAL.ClinicContext context)
        {
            //context.Tariffs.AddOrUpdate(
            //       t => t.ID,
            //       new Tariff { TariffType = "SAMA" },
            //       new Tariff { TariffType = "GRN" },
            //       new Tariff { TariffType = "BHF/NA" }
            //   );
            //context.SaveChanges();

            //context.Modalities.AddOrUpdate(
            //    m => m.ModalityID,
            //    new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 3.94M, TariffID = 1, NappiCode = "491919-003" },
            //    new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 2, NappiCode = "491919-003" },
            //    new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 3, NappiCode = "491919-003" }
            //    );
            //context.SaveChanges();

            //context.Clients.AddOrUpdate(
            //    c => c.ID,
            //    new Person
            //    {
            //        SponsorID = null,
            //        Title = "Mr",
            //        FirstName = "Adewale",
            //        Initials = "",
            //        LastName = "Odunlami",
            //        DateOfBirth = Convert.ToDateTime("15-Jan-1986"),
            //        IsDependant = false
            //    },

            //    new Person
            //    {
            //        SponsorID = 1,
            //        Title = "Mr",
            //        FirstName = "Oreoluwa",
            //        Initials = "O.",
            //        LastName = "Odunlami",
            //        DateOfBirth = Convert.ToDateTime("15-Jan-1986"),
            //        IsDependant = true
            //    }
            //);
            //context.SaveChanges();
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
