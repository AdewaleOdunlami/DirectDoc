using System.Collections.Generic;
using DirectDoc2.Models;

namespace DirectDoc2.ViewModels
{
    public class PatientData
    {
        public IEnumerable<Person> Patients { get; set; }
        public IEnumerable<Person> Dependants { get; set; }
        public IEnumerable<Phone> PhoneNumbers { get; set; }
        public IEnumerable<PostalAddress> PostalAddresses { get; set; }
    }
}