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

        private decimal? grandTotal;
        private int invoiceNumber;

        public int InvoiceID { get; set; }
        //invoice number and date
        [Display(Name = "Invoice Number")]
        public int? InvoiceNumber
        {
            get
            {
                ++invoiceNumber;
                return invoiceNumber;
            }
            private set { }
        }
        [DataType(DataType.DateTime)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate
        {
            get { return DateTime.Now; }
            private set { value = DateTime.Now; }
        }

        public int? PatientID { get; set; }

        [Display(Name = "Patient's D.O.B")]
        public DateTime PatientBirthDate
        {
            get;
            set;
            //{
            //    //retrieve details of patient
            //    var patientDetails = from person in db.Clients
            //                         where person.ID == PersonID
            //                         select person;

            //    if (patientDetails.Any())
            //    {
            //        foreach (var p in patientDetails)
            //        {
            //            this.patientBirthDate = Convert.ToString(p.DateOfBirth);
            //        }
            //    }
            //    return this.patientBirthDate;
            //}
            //private set { ;}
        }

        //invoice to main member
        public int? InvoiceToID { get; set; }
        //insurance name and policy number
        [Display(Name = "Insurance Name")]
        public string InsuranceName
        {
            get;
            set;
            //{
            //    //determine membership status
            //    var isMainMember = from patient in db.Clients
            //                       where patient.ID == PersonID && patient.SponsorID == null
            //                       select patient;

            //    var insuranceDetails = from person in db.MedicalAids
            //                        join mainmember in isMainMember on person.PersonID equals mainmember.ID
            //                        select person;

            //    //retrieve mainmember's fullname 
            //    if (insuranceDetails.Any())
            //    {
            //        foreach (var person in insuranceDetails)
            //        {
            //            this.insuranceName = Convert.ToString(person.NameOfAid);
            //        }
            //    }
            //return this.insuranceName;
            //}
            //private set { ;}
        }

        [Display(Name = "Policy Number")]
        public string PolicyNumber
        {
            get;
            set;
            //{
            //    //determine membership status
            //    var isMainMember = from patient in db.Clients
            //                       where patient.ID == PersonID && patient.SponsorID == null
            //                       select patient;

            //    var insuranceDetails = from person in db.MedicalAids
            //                           join mainmember in isMainMember on person.PersonID equals mainmember.ID
            //                           select person;

            //    //retrieve mainmember's fullname 
            //    if (insuranceDetails.Any())
            //    {
            //        foreach (var person in insuranceDetails)
            //        {
            //            this.policyNumber = Convert.ToString(person.PolicyNumber);
            //        }
            //    }
            //    return this.policyNumber;
            //}
            //private set { ;}
        }

        [Display(Name = "Address")]
        public string Address
        {
            get;
            set;
            //{
            //    //retrieve details of mainmember
            //    var isMainMember = from patient in db.Clients
            //                       where patient.ID == PersonID && patient.SponsorID == null
            //                       select patient;

            //    //retrieve address of mainmember
            //    var address = from patient in db.PostalAddresses
            //                  join person in isMainMember 
            //                    on patient.PersonID equals person.ID into addressdetails
            //                  from ad in addressdetails
            //                    select patient;

            //    if (address.Any())
            //    {
            //        foreach (var person in address)
            //        {
            //            this.mainMemberAddress = Convert.ToString(person.FullAddress);
            //        }
            //    }
            //    //retrieve dependant's mainmember
            //    else
            //    {
            //        var isDependant = from patient in db.Clients
            //                           where patient.ID == PersonID && patient.SponsorID != null
            //                           select patient;

            //        //retrieve address of mainmember
            //        var _address = from patient in db.PostalAddresses
            //                      join person in isDependant
            //                        on patient.PersonID equals person.SponsorID into addressdetails
            //                      from ad in addressdetails
            //                      select patient;

            //        if (_address.Any())
            //        {
            //            foreach (var person in _address)
            //            {
            //                this.mainMemberAddress = Convert.ToString(person.FullAddress);
            //            }
            //        }
            //    }

            //    return this.mainMemberAddress;
            //}
            //private set { ;}
        }

        

        public decimal? Total
        {
            get
            {
                foreach(Consultation consultation in db.Consultations)
                {
                    grandTotal += consultation.SubTotal;
                }
                return grandTotal;
            }
            private set { ;}
        }

        public Person Patient { get; set; }
        public Person InvoiceTo { get; set; }
    }
}