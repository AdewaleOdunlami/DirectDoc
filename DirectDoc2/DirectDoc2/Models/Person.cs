using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectDoc2.Models
{
    public enum Titles 
    {
        Mr, Mrs, Dr, Professor, Ms, Jr
    }

    public class Person
    {
        public int ID { get; set; }
        public int? SponsorID { get; set; }
        public string Title { get; set; }

        [Display(Name = "First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string FirstName { get; set; }

        public string Initials { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string LastName { get; set; }
  
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public bool Dependant { get; set; }
        public List<Phone> Contacts { get; set; }

    }

}