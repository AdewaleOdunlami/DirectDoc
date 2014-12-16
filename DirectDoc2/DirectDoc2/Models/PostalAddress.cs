
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DirectDoc2.Models
{
    public class PostalAddress
    {  
        public int PostalAddressID { get; set; }
        public int PersonID { get; set; }
        public int BoxNumber { get; set; }
        public string City { get; set; } 
        public string Country { get; set; }

        public Person Person { get; set; }
    }
}
