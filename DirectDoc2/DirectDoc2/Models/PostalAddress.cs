
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DirectDoc2.Models
{
    public class PostalAddress
    {  
        public int PostalAddressID { get; set; }
        [Display(Name="Main member")]
        public int? PersonID { get; set; }
        [Display(Name="Box Number")]
        public int? BoxNumber { get; set; }
        [Required]
        [Display(Name="City/State/Province")]
        [StringLength(500,MinimumLength=2,ErrorMessage="Please enter the name of a city")]
        public string City { get; set; }
        public string Country { get; set; }
        public string FullAddress 
        {
            get { return BoxNumber + " " + City + " " + Country;}
            private set { ;}
        }

        public virtual Person Person { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
