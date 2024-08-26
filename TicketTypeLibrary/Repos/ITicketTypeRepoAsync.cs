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
        Task<List<TicketType>> GetAllTicketType();
        Task<List<TicketType>> GetAllbyAssignedEmpId(int EmpId);
        Task<TicketType> GetTicketbyId(int TypeId);
        Task<Employee> GetEmployeebyId(int empId);
        Task<TicketPriority> GetTicketPrioritybyId(int priorityId);
        Task InsertTicketType(TicketType type);
        Task UpdateTicketType(int TypeId, TicketType type);
        Task DeleteTicketType(int TypeId);
        Task AddEmployee(Employee Emp);
        Task DeleteEmployee(int EmpId);
        Task AddPriority(TicketPriority priority);
        Task DeletePriority(int priorityId);    
    }
}
