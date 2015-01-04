
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DirectDoc2.Models
{
    public class Modality
    {
        public int ModalityID { get; set; }
        [Display(Name="Tariff")]
        public int TariffID { get; set; }

        [Display(Name = "Code")]
        public string ModalityCode { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public virtual Tariff Tariff { get; set; }

    }
}
