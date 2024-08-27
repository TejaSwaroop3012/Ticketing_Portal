using EmployeeLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Repos
{
    public interface IEmployeeRepoAsync
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetByEmpId(int empId);
        Task InsertEmployee(Employee employee);
        Task UpdateEmployee(int empId, Employee employee);
        Task DeleteEmployee(int empId);

    }
}
