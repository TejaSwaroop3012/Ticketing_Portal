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
        Task<List<Ticket>> GetAllTickets(); 
        Task<List<Ticket>> GetTicketByEmpId(int empId);  
        Task<Employee> GetEmployeeById(int empId);  
        Task<List<Ticket>> GetTicketByTicketTypeId(int typeId); 
        Task<TicketType> GetTicketTypeById(int tyepId);  
        Task<List<Ticket>> GetTicketByTicketTypeIdandEmployeeId(int empId, int typeId); 
        Task<Ticket> GetTicketById(int ticketId); 
        Task AddTicket(Ticket ticket);  
        Task UpdateTicket(int ticketId, Ticket ticket);  
        Task DeleteTicket(int ticketId);  
        Task AddEmployee(Employee employee);  
        Task DeleteEmployee(int empId);  
        Task AddTicketType(TicketType type);  
        Task DeleteTicketType(int typeId);  
    }
}
