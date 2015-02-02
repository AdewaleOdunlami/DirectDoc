
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DirectDoc2.DAL;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Collections;
namespace DirectDoc2.Models
{
    public class Modality
    {
        ClinicContext db = new ClinicContext();
        List<String> names = new List<string>();
        private string codeDescription;

        public int ModalityID { get; set; }
        [Display(Name="Tariff")]
        public int? TariffID { get; set; }
        [Display(Name="Nappi Code")]
        public string NappiCode { get; set; }
        [Display(Name = "Code")]
        public string ModalityCode { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CodeDescription
        {
            get 
            {    
                return codeDescription;
            }
            private set 
            {
                var tariffName = from tariff in db.Tariffs
                                 join m in db.Modalities on tariff.ID equals m.TariffID into tariffdetails
                                 from td in tariffdetails
                                 where td.TariffID == this.TariffID
                                 select tariff.TariffType;

                if (tariffName.Any())
                {
                    foreach (var tariff in tariffName)
                    {
                        names.Add(tariff);
                    }
                }
                else
                {
                    names.Add(" ");
                }

                codeDescription = ModalityCode + " " + Description + " " + names[0];
            }
        }

        public virtual Tariff Tariff { get; set; }

    }
}
