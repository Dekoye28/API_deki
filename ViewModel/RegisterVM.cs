using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class RegisterVM
    {

        public string nik { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string password { get; set; }
        public string Degre { get; set; }
        public string GPA { get; set; }
        public string University_id { get; set; }
    }
    public enum Gender { 
        Male, Female
    }
}
