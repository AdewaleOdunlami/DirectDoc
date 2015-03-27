using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DirectDoc2.Models
{
    public class ChequePayment : Payment
    {
        [Required]
        [Display(Name = "Cheque Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOnCheck { get; set; }
        public int? CheckNumber { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string PayableTo { get; set; }
    }
}