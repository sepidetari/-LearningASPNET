using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagment.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        public Employee GetEmployee(int Id)
        {
            return _context.Employees.Find(Id);
        }

        public IEnumerable<Employee> getAllEmployees()
        {
            return _context.Employees;
        }

        public Employee Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
           var employee = _context.Employees.Attach(employeeChanges);
           employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           _context.SaveChanges();
           return employeeChanges;
        }

        public Employee Delete(int Id)
        {
            Employee employee = _context.Employees.Find(Id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }

            return employee;
        }
    }
}
