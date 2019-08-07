using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public static class ModelBuilderExtenstion
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
              new Employee
              {
                  Id = 2,
                  Name = "Mary",
                  Department = Dept.HR,
                  Email = "Mary@gmail.com"
              },
              new Employee
              {
                  Id = 1,
                  Name = "Sepide",
                  Department = Dept.IT,
                  Email = "Sepide@gmail.com"
              }
           );
        } 
    }
}
