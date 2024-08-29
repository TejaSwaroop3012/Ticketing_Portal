using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketLibrary.Models;

namespace TicketLibrary.Repos
{
    public interface ITicketRepoAsync
    {
        Task<List<Ticket>> GetAllTicketsAsync(); 
        Task<List<Ticket>> GetTicketByEmpIdAsync(int empId);  
        Task<Employee> GetEmployeeByIdAsync(int empId);  
        Task<List<Ticket>> GetTicketByTicketTypeIdAsync(int typeId); 
        Task<TicketType> GetTicketTypeByIdAsync(int tyepId);  
        Task<List<Ticket>> GetTicketByTicketTypeIdandEmployeeIdAsync(int empId, int typeId); 
        Task<Ticket> GetTicketByIdAsync(int ticketId); 
        Task AddTicketAsync(Ticket ticket);  
        Task UpdateTicketAsync(int ticketId, Ticket ticket);  
        Task DeleteTicketAsync(int ticketId);  
        Task AddEmployeeAsync(Employee employee);  
        Task DeleteEmployeeAsync(int empId);  
        Task AddTicketTypeAsync(TicketType type);  
        Task DeleteTicketTypeAsync(int typeId);  
    }
}
