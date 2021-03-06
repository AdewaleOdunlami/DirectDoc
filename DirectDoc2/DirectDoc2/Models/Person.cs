﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DirectDoc2.Models
{
    public class Person
    {
        public int ID { get; set; }
        [Display(Name="Main member")]
        public int? SponsorID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Field cannot be empty")]
        public string FirstName { get; set; }
        public string Initials { get; set; }  
        [Required]
        [Display(Name = "Lastname")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Field cannot be empty")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Display(Name="Is a Dependant")]
        public bool IsDependant { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FullName 
        {
            get { return Title + " " + FirstName + " " + Initials + " " + LastName; }
            private set { }
        }

        public virtual Person Sponsor { get; set; }

        public virtual ICollection<Phone> PhoneNumbers { get; set; }
        public virtual ICollection<PostalAddress> PostalAddresses { get; set; }
        public virtual ICollection<Person> Dependants { get; set; }
        public virtual ICollection<Consultation> Consultations { get; set; }
    }
}