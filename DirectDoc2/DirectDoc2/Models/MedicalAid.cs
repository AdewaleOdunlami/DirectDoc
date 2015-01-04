
using System.ComponentModel.DataAnnotations;
namespace DirectDoc2.Models
{
    public class MedicalAid
    {
        public int MedicalAidID { get; set; }
        public int PersonID { get; set; } 
        public string NameOfAid { get; set; }
        public int PolicyNumber { get; set; }
        public int TariffID { get; set; }
       
        public Person Person { get; set; }
        public Tariff Tariff { get; set; }
    }
}
