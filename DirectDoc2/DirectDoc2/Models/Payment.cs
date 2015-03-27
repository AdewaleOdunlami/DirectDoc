using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DirectDoc2.Models
{
    public abstract class Payment
    {
        public int PaymentID { get; set; }
        [Required]
        public int? PersonID { get; set; }
        public int? InvoiceID { get; set; }
        [Required]
        [Display(Name="Payment Date")]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime PaymentDate { get; set; }
        public decimal? Amount { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Person Person { get; set; }
    }

}