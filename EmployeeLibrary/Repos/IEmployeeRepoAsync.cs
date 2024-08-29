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
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetByEmpIdAsync(int empId);
        Task InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int empId, Employee employee);
        Task DeleteEmployeeAsync(int empId);

    }
}
