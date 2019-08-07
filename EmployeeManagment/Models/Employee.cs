using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]  //we declare the Name mandatory by specifying [Required] attribute
        [MaxLength(50, ErrorMessage = "Name can not exit 50 characters")]
        public String Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9.+-]+@[a-zA-Z0-9-.]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "office Email")]
        public String Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        public String PhotoPath { get; set; }
    }
}
