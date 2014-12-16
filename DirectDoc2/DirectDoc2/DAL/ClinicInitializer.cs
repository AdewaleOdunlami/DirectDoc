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
        }
    }
}