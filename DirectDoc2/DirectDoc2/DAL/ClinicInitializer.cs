using DirectDoc2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DirectDoc2.DAL
{
    public class ClinicInitializer 
        : System.Data.Entity.DropCreateDatabaseIfModelChanges<ClinicContext>
    {
        
        protected override void Seed(ClinicContext context)
        {
            var tariff = new List<Tariff>
            {
                new Tariff{TariffType="SAMA"},
                new Tariff{TariffType="GRN"},
                new Tariff{TariffType="BHF/NA"}
            };

            tariff.ForEach(t => context.Tariffs.Add(t));
            context.SaveChanges();
                      
            //base.Seed(context);

            var modality = new List<Modality>
            {
                new Modality{ModalityCode="2345", Description="I know you", Price=3.94M, TariffID=1},
                new Modality{ModalityCode="2058", Description="I saw you", Price=2.52M, TariffID=2}
            };

            modality.ForEach(t => context.Modalities.Add(t));
            context.SaveChanges();

            var client = new List<Person>
            {
                new Person{
                    SponsorID = null, Title = "Mr", FirstName = "Adewale", Initials = "",
                    LastName = "Odunlami", DateOfBirth = Convert.ToDateTime("15-Jan-1986"), Dependant = false},

                new Person{
                    SponsorID = 1, Title = "Mr", FirstName = "Oreoluwa", Initials = "O.",
                    LastName = "Odunlami", DateOfBirth = Convert.ToDateTime("15-Jan-1986"), Dependant = true}
            };

            client.ForEach(t => context.Clients.Add(t));
            context.SaveChanges();
        }
    }
}