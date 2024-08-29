using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLibrary.Repos
{
    public class EFEmployeeRepoAsync : IEmployeeRepoAsync
    {
        EmployeeDBContext ctx = new EmployeeDBContext();
        public async Task DeleteEmployeeAsync(int empId)
        {
            Employee employee = await GetByEmpIdAsync(empId);
            try
            {
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            List<Employee> employees = await ctx.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetByEmpIdAsync(int empId)
        {
            try
            {
                Employee employee = await (from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                return employee;
            }
            catch
            {
                throw new EmployeeException("No such employeeid");
            }
        }

        public async Task InsertEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }

        public async Task UpdateEmployeeAsync(int empId, Employee employee)
        {
            try
            {
                Employee emp2edit = await GetByEmpIdAsync(empId);
                emp2edit.FirstName = employee.FirstName;
                emp2edit.LastName = employee.LastName;
                emp2edit.EmailId = employee.EmailId;
                emp2edit.MobileNo = employee.MobileNo;
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new EmployeeException(ex.Message);
            }
        }
    }
}
