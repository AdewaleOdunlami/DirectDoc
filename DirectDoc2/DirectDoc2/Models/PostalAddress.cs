
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DirectDoc2.Models
{
    public class PostalAddress
    {  
        public int PostalAddressID { get; set; }
        public int? PersonID { get; set; }
        public int? BoxNumber { get; set; }
        [Required]
        [StringLength(500,MinimumLength=2,ErrorMessage="Please enter the name of a city")]
        public string City { get; set; }

        public string Country { get; set; }

        public Person Person { get; set; }
    }
}
