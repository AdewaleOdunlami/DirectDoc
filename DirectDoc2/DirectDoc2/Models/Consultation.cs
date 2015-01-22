using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DirectDoc2.Models
{
    public class Consultation
    {
        public int ConsultationID { get; set; }
        [Required]
        [Display(Name="Patient")]
        public int? PersonID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ConsultationDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Time")]
        [DisplayFormat(DataFormatString="{0:t}", ApplyFormatInEditMode = true)]
        public DateTime ConsultationTime { get; set; }
        [Required]
        [Display(Name = "Code")]
        public int? ModalityID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,500)]
        public int? Quantity { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        [DisplayFormat(DataFormatString= "{0:C}")]
        public decimal? UnitPrice { get; set; }
        [Display(Name = "Total")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal? SubTotal
        {
            get { return Convert.ToDecimal(Quantity) * UnitPrice; }
            private set { }
        }

        public Person Person { get; set; }
        public Modality Modality { get; set; }
    }
}