using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DirectDoc2.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int PersonID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Invoice Invoice { get; set; }
        public Person Person { get; set; }
    }

    public class CreditCard : Payment
    {
        public string NameOnCard { get; set; }
        [CreditCard]
        public int CardNumber { get; set; }
        public string CardType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardIssuer { get; set; }
    }

    public class Cash : Payment
    {
    }

    public class Check : Payment
    {
        public DateTime DateOnCheck { get; set; }
        public int CheckNumber { get; set; }
        public string BankName { get; set; }
        public string PayableTo { get; set; }
    }
}