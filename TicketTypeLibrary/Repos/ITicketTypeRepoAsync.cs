using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTypeLibrary.Models;

namespace TicketTypeLibrary.Repos
{
    public interface ITicketTypeRepoAsync
    {
        Task<List<TicketType>> GetAllTicketTypeAsync();
        Task<List<TicketType>> GetAllbyAssignedEmpIdAsync(int EmpId);
        Task<TicketType> GetTicketbyIdAsync(int TypeId);
        Task<Employee> GetEmployeebyIdAsync(int empId);
        Task<TicketPriority> GetTicketPrioritybyIdAsync(int priorityId);
        Task InsertTicketTypeAsync(TicketType type);
        Task UpdateTicketTypeAsync(int TypeId, TicketType type);
        Task DeleteTicketTypeAsync(int TypeId);
        Task AddEmployeeAsync(Employee Emp);
        Task DeleteEmployeeAsync(int EmpId);
        Task AddPriorityAsync(TicketPriority priority);
        Task DeletePriorityAsync(int priorityId);    
    }
}
