
namespace DirectDoc2.Models
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public int PersonID { get; set; }

        public enum PhoneType
        { 
            Cell, Home, Work
        };
        public int AreaCode { get; set; }
        public int Number { get; set; }

        public Person Person { get; set; }
    }
}
