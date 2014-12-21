using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DirectDoc2.Models
{
    public enum Titles 
    {
        Mr, Mrs, Dr, Professor, Ms, Jr
    }

    public class Person
    {
        public Person(string firstName, string initials,
             string lastName, DateTime dateOfBirth, string fullname = null, string sponsor = null, bool dependant = false)
        {
            this.sponsor = sponsor;
            this.firstName = firstName;
            this.initials = initials;
            this.lastName = lastName;            
            this.dateOfBirth = dateOfBirth;
            this.dependant = dependant;
            this.fullName = firstName + lastName;
        }

        public Person()
        {
        }

        private string sponsor = null;
        private string fullName = null;
        private string firstName = null;
        private string initials = null;
        private string lastName = null;
        private DateTime dateOfBirth = DateTime.Now;
        private bool dependant = false;
        
        public int ID { get; set; }
        public int? SponsorID { get; set; }
        public string Title { get; set; }
        public string FirstName 
        {
            get { return firstName; }
            set { value = firstName; }
        }
        public string Initials
        {
            get { return initials; }
            set { value = initials; }
        }
        public string LastName
        {
            get { return lastName; }
            set { value = lastName; }
        }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { value = dateOfBirth; }
        }
        public bool Dependant
        {
            get { return dependant; }
            set { value = dependant; }
        }
        public List<Phone> Contacts { get; set; }
        public string FullName
        {
            get { return fullName; }
            set { value = fullName; }
        }
        
        public virtual Person Sponsor { get; set; }
    }

}