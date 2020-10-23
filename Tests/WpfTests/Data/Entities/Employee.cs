using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WpfTests.Data.Entities
{
    class Employee
    {        
        public string FIO { get; set; }
        public string Email { get; set; }
        [Required,MaxLength(20)]
        public string PhoneNumber { get; set; }
    }
}
