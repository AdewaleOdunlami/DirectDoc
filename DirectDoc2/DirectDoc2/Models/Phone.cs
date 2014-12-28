
using System.ComponentModel.DataAnnotations;
namespace DirectDoc2.Models
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public int? PersonID { get; set; }
        [Required]
        [Display(Name="Phone type")]
        public string PhoneType { get; set; }
        [Required]
        [Display(Name="Area code")]
        [Range(1,1000,ErrorMessage="Please enter a valid area code")]
        public int? AreaCode { get; set; }
        [Required]
        [Display(Name="Phone number")]
        public int? Number { get; set; }

        public Person Person { get; set; }
    }
}
