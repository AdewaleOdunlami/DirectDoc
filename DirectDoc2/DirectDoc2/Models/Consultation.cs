using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DirectDoc2.DAL;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectDoc2.Models
{
    public class Consultation
    {
        ClinicContext db = new ClinicContext();
        private decimal unitPrice;
        private string modalityDescription;

        public int ConsultationID { get; set; }
        [Required]
        [Display(Name="Patient")]
        public int? PersonID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ConsultationDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString="{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ConsultationTime { get; set; }
        [Required]
        [Display(Name = "Modality Code")]
        public int? ModalityID { get; set; }
        [Required]
        [Range(1, 500)]
        public int? Quantity { get; set; }
        [Display(Name="Description")]
        public string ModalityDescription
        {
            //get;
            //set;
            get
            {
                return modalityDescription;
            }
            private set
            {
                var getModalityDescription = from modality in db.Modalities
                                                where modality.ModalityID == this.ModalityID
                                                select modality.Description;

                if(getModalityDescription.Any())
                {
                    foreach(var modality in getModalityDescription)
                    {
                        this.modalityDescription = modality;
                    }
                }
                else 
                {
                    this.modalityDescription = "No description";
                }
            }
        }

        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString= "{0:C}")]
        public decimal? UnitPrice 
        {
            get
            {
                return unitPrice;
            }
            private set 
            {
                //determine if patient is a dependant
                var isDependant = from person in db.Clients
                                   where person.IsDependant == true && person.ID == this.PersonID
                                   select person;

                if(isDependant.Any())
                {
                    //get main member
                    var sponsor = from person in db.Clients
                                  join dependant in isDependant
                                    on person.ID equals dependant.SponsorID into spondordetails
                                    from sd in spondordetails
                                    select sd;

                    //get main members tariff
                    var tariff = from person in db.MedicalAids
                                 join mainmember in sponsor 
                                    on person.PersonID equals mainmember.SponsorID into tariffdetails
                                from td in tariffdetails
                                select person;

                    //get tariff price for chosen modality
                    var price = from c in db.Modalities
                                join t in tariff 
                                    on c.TariffID equals t.TariffID into pricedetails
                                from pd in pricedetails
                                select c;

                    foreach(var p in price)
                    {
                        this.unitPrice = p.Price;
                    }  
                }
                else
                {
                    //patient is not a dependant
                    var medicalinfo = from med in db.MedicalAids
                                      where med.PersonID == this.PersonID
                                      select med;

                    var tariffinfo = from t in db.Modalities
                                     join m in medicalinfo
                                        on t.TariffID equals m.TariffID into tariffdetails
                                        from td in tariffdetails
                                        select t;

                    if(tariffinfo.Any())
                    {
                        foreach(var t in tariffinfo)
                        {
                            this.unitPrice = t.Price;
                        }
                    }
                }
            }
        }
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString="{0:C}")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? SubTotal
        {
            get { return Convert.ToDecimal(Quantity) * UnitPrice; }
            private set { }
        }

        public Person Person { get; set; }
        public Modality Modality { get; set; }
    }
}