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
        Task<List<Ticket>> GetAllTickets(); //Index1
        Task<List<Ticket>> GetTicketByEmpId(int empId);  //Index2
        Task<Employee> GetEmployeeById(int empId);  //Details1
        Task<List<Ticket>> GetTicketByTicketTypeId(int typeId); //Index3
        Task<TicketType> GetTicketTypeById(int tyepId);  //Details2
        Task<List<Ticket>> GetTicketByTicketTypeIdandEmployeeId(int empId, int typeId); //Index4
        Task<Ticket> GetTicketById(int ticketId); //Details3
        Task AddTicket(Ticket ticket);  //Create1
        Task UpdateTicket(int ticketId, Ticket ticket);  //Edit
        Task DeleteTicket(int ticketId);  //Delete1
        Task AddEmployee(Employee employee);  //Create2
        Task DeleteEmployee(int empId);  //Delete2
        Task AddTicketType(TicketType type);  //Create3
        Task DeleteTicketType(int typeId);  //Delete3
    }
}
