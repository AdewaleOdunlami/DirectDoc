using DirectDoc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DirectDoc2.DAL
{
    public class ClinicInitializer
        : System.Data.Entity.DropCreateDatabaseIfModelChanges<ClinicContext>
    {
        protected override void Seed(ClinicContext context)
        {
                //context.Tariffs.AddOrUpdate(
                //    t => t.ID,
                //    new Tariff { TariffType = "SAMA" },
                //    new Tariff { TariffType = "GRN" },
                //    new Tariff { TariffType = "BHF/NA" }
                //);
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
                //    new Person{
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

            //}
        //    var tariff = new List<Tariff>
        //    {
        //        new Tariff{TariffType="SAMA"},
        //        new Tariff{TariffType="GRN"},
        //        new Tariff{TariffType="BHF/NA"}
        //    };

        //    tariff.ForEach(t => context.Tariffs.Add(t));
        //    context.SaveChanges();

        //    //base.Seed(context);


        //    var modality = new List<Modality>
        //    {
        //        new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 3.94M, TariffID = 1, NappiCode = "491919-003" },
        //        new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 2, NappiCode = "491919-003" },
        //        new Modality { ModalityCode = "0201", Description = "Cost 5ml Syringe", Price = 2.52M, TariffID = 3, NappiCode = "491919-003" }
        //    };

        //    modality.ForEach(t => context.Modalities.Add(t));
        //    context.SaveChanges();

        //    var client = new List<Person>
        //    {
        //        new Person{
        //            SponsorID = null, Title = "Mr", FirstName = "Adewale", Initials = "",
        //            LastName = "Odunlami", DateOfBirth = Convert.ToDateTime("15-Jan-1986"), IsDependant = false},

        //        new Person{
        //            SponsorID = 1, Title = "Mr", FirstName = "Oreoluwa", Initials = "O.",
        //            LastName = "Odunlami", DateOfBirth = Convert.ToDateTime("15-Jan-1986"), IsDependant = true}
        //    };

        //    client.ForEach(t => context.Clients.Add(t));
        //    context.SaveChanges();
        }
    }
}