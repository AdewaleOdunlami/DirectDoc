using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectDoc2.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string InvoiceTo { get; set; }

        public int InvoiceNumber { get; set; }

        public Person FullNameOfPatient { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime InvoiceTime { get; set; }

        public int ModalityID { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        //public Physician Physician { get; set; }
        public Modality Modality { get; set; }

        public List<Modality> Modalities { get; set; }

        public Invoice()
        {
            Person person= new Person();
            InvoiceTo = person.FirstName + person.LastName;
        }
    }
}