using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectDoc2.Models
{
    public class Invoice
    {
        public Invoice()
        {
            Person sponsor = new Person();
            Person patient = new Person();

            fullNameOfSponsor = sponsor.FirstName + " " + sponsor.Initials + " " + sponsor.LastName;
            fullNameOfPatient = patient.FirstName + " " + patient.Initials + " " + patient.LastName;

            modalitiesList = new List<Modality>();
        }

        private List<Modality> modalitiesList;
        private string fullNameOfPatient;
        private string fullNameOfSponsor;
    
        public int InvoiceID { get; set; }
        public int InvoiceNumber { get; set; }
        public string InvoiceTo
        {
            get { return fullNameOfSponsor; }
            set { value = fullNameOfSponsor; }
        }
        public string FullNameOfPatient
        {
            get { return fullNameOfPatient; }
            set { value = fullNameOfPatient; }
        }
        public DateTime InvoiceDate { get; set; }
        public DateTime InvoiceTime { get; set; }
        //public Physician Physician { get; set; }
        public List<Modality> Modalities 
        { 
            get { return modalitiesList; }
            set { value = modalitiesList; }
        }
        public decimal TotalAmount { get; set; }
    }
}