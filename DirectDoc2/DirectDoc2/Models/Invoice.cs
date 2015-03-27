using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DirectDoc2.DAL;
using DirectDoc2.Models;
using System.Linq;

namespace DirectDoc2.Models
{
    public class Invoice
    {
        ClinicContext db = new ClinicContext();
        private decimal grandTotal;
        private string insuranceName;
        private string policyNumber;
        private string invoiceTo;
        private string mainMemberAddress;
        private int invoiceNumber;
        private DateTime? patientBirthDate;
        
        
        public int InvoiceID { get; set; }
        public int? PersonID { get; set; }
        [Display(Name = "Invoice Number")]
        public int? InvoiceNumber
        {
            get { return invoiceNumber; }
            private set { ++invoiceNumber; }
        }
        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime InvoiceDate { get; set; }

        //insurance name and policy number
        [Display(Name = "Insurance Name")]
        public string NameOfAid
        {
            get
            {
                return insuranceName;
            }
            private set 
            {
                var isMainMember = from patient in db.Clients
                                   where patient.ID == PersonID && patient.SponsorID == null
                                   select patient;

                var insuranceName = from person in db.MedicalAids
                                    join mainmember in isMainMember on person.PersonID equals mainmember.ID
                                        into insurancedetails
                                    from ins in insurancedetails
                                    select person;

                //retrieve mainmember's fullname 
                if (insuranceName.Any())
                {
                    foreach (var person in insuranceName)
                    {
                        this.insuranceName = person.NameOfAid.ToString();
                    }
                }
            }
        }

        [Display(Name = "Policy Number")]
        public string PolicyNumber
        {
            get
            {
                return policyNumber;
            }
            private set
            {
                var isMainMember = from patient in db.Clients
                                   where patient.ID == PersonID && patient.SponsorID == null
                                   select patient;

                var policyNumber = from person in db.MedicalAids
                                   join mainmember in isMainMember on person.PersonID equals mainmember.ID
                                       into insurancedetails
                                   from ins in insurancedetails
                                   select person;

                //retrieve mainmember's fullname 
                if (policyNumber.Any())
                {
                    foreach (var person in policyNumber)
                    {
                        this.policyNumber = person.PolicyNumber.ToString();
                    }
                }
            }
        }

        //invoice to: main member name and address
        [Display(Name = "Invoice To")]
        public string InvoiceTo
        {
            get
            {
                return this.invoiceTo;
            }
            private set
            {
                //retrieve details of mainmember
                var isMainMember = from patient in db.Clients
                                   where patient.ID == PersonID && patient.SponsorID == null
                                   select patient;

                //retrieve mainmember's fullname 
                if (isMainMember.Any())
                {
                    foreach (var person in isMainMember)
                    {
                        this.invoiceTo = person.FullName.ToString();
                    }
                }
                //retrieve dependant's fullname
                else
                {
                    //retrieve details of mainmember
                    var isDependant = from person in db.Clients
                                       where person.ID == PersonID && person.SponsorID != null
                                       select person;

                    var sponsor =  from dependant in isDependant 
                                   join mainmember in db.Clients 
                                    on dependant.SponsorID equals mainmember.ID into sponsordetails
                                      from sd in sponsordetails
                                      select sd;

                    if (sponsor.Any())
                    {
                        foreach (var person in sponsor)
                        {
                            this.invoiceTo = person.FullName.ToString();
                        }
                    }
                }
            }
        }

        [Display(Name = "Postal Address")]
        public string Address
        {
            //set address
            private set
            {
                //Is mainmember
                var isMainMember = from patient in db.PostalAddresses
                                    where patient.PersonID == PersonID
                                    select patient;

                //retrieve mainmember's address 
                if (isMainMember.Any())
                {
                    foreach (var person in isMainMember)
                    {
                        this.mainMemberAddress = Convert.ToString(person.FullAddress);
                    }
                }
                
                else
                {
                    //Is dependant
                    var isDependant = from dependant in db.Clients
                                         where dependant.ID == PersonID
                                            select dependant;

                    //get dependant's mainmember
                    var mainmember = from dependant in isDependant
                                     join sponsor in db.PostalAddresses
                                     on dependant.SponsorID equals sponsor.PersonID into getaddress
                                     from dependantaddress in getaddress
                                     select dependantaddress;

                    //retrieve dependant's address
                    if (mainmember.Any())
                    {
                        foreach (var person in mainmember)
                        {   
                            this.mainMemberAddress = Convert.ToString(person.FullAddress);
                        }
                    }
                }
            }

            //get address
            get { return this.mainMemberAddress; } 
        }

        [Display(Name = "Patient's D.O.B")]
        [DataType(DataType.Date)]
        public DateTime? PatientBirthDate
        {
            private set
            {
                //retrieve details of patient
                var patientDetails = from patient in db.Invoices
                                     join thepatient in db.Clients
                                     on patient.PersonID equals thepatient.ID into details
                                     from d in details
                                     select d;

                if(patientDetails.Any())
                {
                   
                    foreach (var patient in patientDetails)
                    {
                        try
                        {
                            this.patientBirthDate = patient.DateOfBirth;
                        }
                        catch(System.ArgumentNullException e)
                        {
                            this.patientBirthDate = null;
                        }   
                    }
                } 
            }

            get { return this.patientBirthDate; }
            
        }

        public decimal? Total
        {
            private set
            {
                var consults = from c in db.Consultations
                               where c.PersonID == this.PersonID 
                                    && c.ConsultationDate == this.InvoiceDate
                               select c;

                if (consults.Any())
                {
                    foreach (var consult in consults)
                    {
                        grandTotal += Convert.ToDecimal(consult.SubTotal);
                    }
                }
                else
                {
                    ;
                }
            }
            get { return grandTotal; }
        }

        public virtual Person Person { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}