using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectDoc2.Models
{
    public class MedicalAid
    {
        public int MedicalAidID { get; set; }
        [Required]
        [Display(Name="Main member")]
        public int? PersonID { get; set; } 
        [Required]
        [Display(Name="Insurance Name")]
        public string NameOfAid { get; set; }
        [Required]
        [Display(Name="Policy Number")]
        public int? PolicyNumber { get; set; }
        [Required]
        [Display(Name="Tariff")]
        public int? TariffID { get; set; }
       
        public virtual Person Person { get; set; }
        public virtual Tariff Tariff { get; set; }

        public virtual ICollection<Person> Subscribers { get; set; }
    }
}
