using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>
            { new Employee(){Id = 1, Name = "Sepide", Department = Dept.IT, Email = "Sep@gmail.com" },
             new Employee(){Id = 2, Name = "Ariyan", Department = Dept.IT, Email = "Ariyan@gmail.com" },
             new Employee(){Id = 3, Name = "Philipp", Department = Dept.HR , Email = "Phil@gmail.com" }
            };
        }

        public Employee GetEmployee(int Id)
        {
            //throw new NotImplementedException();
            return _employeeList.FirstOrDefault(e => e.Id == Id);

        }

        public IEnumerable<Employee> getAllEmployees()
        {
            return _employeeList;
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
             _employeeList.Add(employee);
             return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employeeChanges != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }

        public Employee Delete(int Id)
        {
          Employee employee =  _employeeList.FirstOrDefault(e => e.Id == Id);
          if (employee != null)
          {
              _employeeList.Remove(employee);
          }

          return employee;
        }
    }
}
