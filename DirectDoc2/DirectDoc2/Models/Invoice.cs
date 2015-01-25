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
        private string patientName;
        private string patientBirthDate;

        public int InvoiceID { get; set; }
        public int? PersonID { get; set; }
        //invoice number and date
        private int invoiceNumber;
        [Display(Name = "Invoice Number")]
        public int? InvoiceNumber
        {
            get
            { 
                return invoiceNumber;
            }
            private set 
            {
                ++invoiceNumber;
            }
        }
        [DataType(DataType.Date)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate
        {
            get;
            set;
        }

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

                    var sponsor =  from person in db.Clients 
                                   join dependant in isDependant 
                                    on person.ID equals dependant.ID into sponsordetails
                                      from sd in sponsordetails
                                      select person;

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

        //[Display(Name = "Postal Address")]
        //public string Address 
        //{
        //    get
        //    {
        //        //retrieve details of mainmember
        //        var isMainMember = from patient in db.PostalAddresses
        //                           where patient.PersonID == PersonID
        //                           select patient;

        //        //retrieve mainmember's fullname 
        //        if (isMainMember.Any())
        //        {
        //            foreach (var person in isMainMember)
        //            {
        //                this.mainMemberName = Convert.ToString(person.FullAddress) ;
        //            }
        //        }
        //        //retrieve dependant's fullname
        //        else
        //        {
        //            var isDependant = from mainmember in isMainMember
        //                              join dependant in db.Clients
        //                                 on mainmember.ID equals dependant.SponsorID into sponsordetails
        //                              from sd in sponsordetails
        //                              select mainmember;

        //            if (isDependant.Any())
        //            {
        //                foreach (var person in isDependant)
        //                {
        //                    this.mainMemberName = Convert.ToString(person.FullName);
        //                }
        //            }
        //        }

        //        return this.mainMemberName;
        //    }//set; 
        //}

        ////patient name and birthdate
        //[Display(Name = "Name of Patient")]
        //public string FullNameOfPatient 
        //{
        //    get
        //    {
        //        //retrieve details of patient
        //        var patientDetails = from patient in db.Clients
        //                             where patient.ID == PersonID
        //                             select patient.FullName;

        //        //retrieve name of main member if any
        //        var hasMainMember = from person in db.Clients
        //                            join mainmember in patientDetails on person.ID equals mainmember.SponsorID into sponsordetails
        //                            from sd in sponsordetails
        //                            select new { person.FullName };

        //        this.mainMember = Convert.ToString(hasMainMember);
        //        return this.mainMember;
        //    }//set; 
        //}
        //[Display(Name = "Patient's D.O.B")]
        //public DateTime PatientBirthDate 
        //{
        //    get { return db.Clients.;}
        //    set; 
        //}



        public decimal Total
        {
            get
            {
                foreach (Consultation consultation in db.Consultations)
                {
                    grandTotal += Convert.ToDecimal(consultation.SubTotal);
                }
                return grandTotal;
            }
            //set;
        }

        public Person Person { get; set; }
    }
}