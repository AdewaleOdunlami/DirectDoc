
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DirectDoc2.Models
{
    public class Tariff
    {
        public int ID { get; set; }

        [Display(Name="Tariff")]
        public string TariffType { get; set; }
    }
}
