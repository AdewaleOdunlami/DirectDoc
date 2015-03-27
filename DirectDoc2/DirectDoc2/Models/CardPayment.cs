using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DirectDoc2.Models
{
    public class CardPayment : Payment
    {
        [Required]
        public string NameOnCard { get; set; }
        [CreditCard]
        public int? CardNumber { get; set; }
        [Required]
        public string CardType { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string CardIssuer { get; set; }
    }
}