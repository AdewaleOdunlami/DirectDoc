
using System.ComponentModel.DataAnnotations;
namespace DirectDoc2.Models
{
    public class MedicalAid
    {
        public int MedicalAidID { get; set; }
        [Required]
        [Display(Name="Name")]
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
       
        public Person Person { get; set; }
        public Tariff Tariff { get; set; }
    }
}
