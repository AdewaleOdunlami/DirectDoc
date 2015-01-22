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
        private string mainMemberName;
        private string mainMemberAddress;
        private string patientName;
        private string patientBirthDate;

        public int InvoiceID { get; set; }
        public int? PersonID { get; set; }
        //invoice number and date
        int invoiceNumber = 5936;
        [Display(Name = "Invoice Number")]
        public int? InvoiceNumber 
        {
            get 
            {
                ++invoiceNumber;
                return invoiceNumber;
            }
            private set{ }
        }
        [DataType(DataType.DateTime)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate 
        { 
            get {return DateTime.Now;}
            private set { value = DateTime.Now; } 
        }

        //insurance name and policy number
        [Display(Name="Insurance Name")]
        public string NameOfAid 
        {

            get
            {
                var isMainMember = from patient in db.Clients
                                   where patient.ID == PersonID && patient.SponsorID == null
                                   select patient;

                var insuranceName = from person in db.MedicalAids
                                    join mainmember in isMainMember on person.PersonID equals mainmember.ID
                                    select person;

                //retrieve mainmember's fullname 
                if (insuranceName.Any())
                {
                    foreach (var person in insuranceName)
                    {
                        this.insuranceName = Convert.ToString(person.NameOfAid);
                    }
                }
            //    var insuranceName = from medicalaid in db.MedicalAids
            //                    join person in db.Clients on medicalaid.PersonID equals person.SponsorID into personmatch
            //                    from insurancerecord in personmatch
            //                    select new { medicalaid.NameOfAid };
                          
            //    this.insuranceName = Convert.ToString(insuranceName);
            return this.insuranceName;
            }//set;
        }

        //[Display(Name="Policy Number")]
        //public string PolicyNumber 
        //{
        //    //get
        //    //{
        //    //    var policyNumber = from medicalaid in db.MedicalAids
        //    //                    join person in db.Clients on medicalaid.PersonID equals person.SponsorID into personmatch
        //    //                    from insurancerecord in personmatch
        //    //                    select new { medicalaid.PolicyNumber };

        //    //    this.policyNumber = Convert.ToString(policyNumber);
        //    //    return this.policyNumber;
        //    //}//set; 
        //}

        ////invoice to: main member name and address
        //[Display(Name="Main Member")]
        //public string FullNameOfMainMember 
        //{
        //    get
        //    {
        //        //retrieve details of mainmember
        //        var isMainMember = from patient in db.Clients
        //                            where patient.ID == PersonID && patient.SponsorID == null
        //                            select patient;

        //        //retrieve mainmember's fullname 
        //        if(isMainMember.Any())
        //        {
        //            foreach(var person in isMainMember)
        //            {
        //                this.mainMemberName = Convert.ToString(person.FullName);
        //            } 
        //        }
        //        //retrieve dependant's fullname
        //        else
        //        {
        //            var isDependant = from mainmember in isMainMember
        //                               join dependant in db.Clients
        //                                  on mainmember.ID equals dependant.SponsorID into sponsordetails
        //                              from sd in sponsordetails
        //                              select mainmember;

        //            if(isDependant.Any())
        //            {
        //                foreach(var person in isDependant)
        //                {
        //                    this.mainMemberName = Convert.ToString(person.FullName);
        //                }   
        //            }   
        //        }

        //        return this.mainMemberName;
        //    }//set; 
        //}

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
                foreach(Consultation consultation in db.Consultations)
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