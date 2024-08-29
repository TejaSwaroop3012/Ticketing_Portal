using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TicketLibrary.Models;

namespace TicketLibrary.Repos
{
    public class EFTicketRepoAsync:ITicketRepoAsync
    {
        TicketDBContext ctx = new TicketDBContext();

        public async Task AddEmployeeAsync(Employee employee)
        {
            try
            {
                await ctx.Employees.AddAsync(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }

        public async Task AddTicketTypeAsync(TicketType type)
        {
            try
            {
                await ctx.TicketTypes.AddAsync(type);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }
        public async Task AddTicketAsync(Ticket ticket)
        {
            try
            {
                await ctx.Tickets.AddAsync(ticket);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }

        public async Task DeleteEmployeeAsync(int empId)
        {
            Employee emp = await GetEmployeeById(empId);
            try
            {
                ctx.Employees.Remove(emp);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            Ticket ticket = await GetTicketById(ticketId);
            try
            {
                ctx.Tickets.Remove(ticket);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }

        public async Task DeleteTicketTypeAsync(int typeId)
        {
            TicketType type = await GetTicketTypeById(typeId);
            try
            {
                ctx.TicketTypes.Remove(type);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            List<Ticket> tickets = await ctx.Tickets.ToListAsync();
            return tickets;
        }
        public async Task<List<Ticket>> GetTicketByEmpIdAsync(int empId)
        {
            List<Ticket> tickets = await (from t in ctx.Tickets where t.EmpId == empId select t).ToListAsync();
            if (tickets.Count > 0)
            {
                return tickets;
            }
            else
            {
                throw new TicketException("No such Ticket found..");
            }
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                Ticket ticket = await (from t in ctx.Tickets where t.TicketId == ticketId select t).FirstAsync();
                return ticket;
            }
            catch (Exception ex)
            {
                throw new TicketException("No such Ticket found..");
            }

        }

        public async Task<List<Ticket>> GetTicketByTicketTypeIdAsync(int typeId)
        {
            List<Ticket> tickets = await (from t in ctx.Tickets where t.TicketTypeId == typeId select t).ToListAsync();
            if (tickets.Count > 0)
            {
                return tickets;
            }
            else
            {
                throw new TicketException("No such Ticket found with given TicketTypeId");
            }
        }

        public async Task<List<Ticket>> GetTicketByTicketTypeIdandEmployeeIdAsync(int empId, int typeId)
        {
            List<Ticket> tickets = await (from t in ctx.Tickets where t.TicketTypeId == typeId && t.EmpId == empId select t).ToListAsync();
            if (tickets.Count > 0)
            {
                return tickets;
            }
            else
            {
                throw new TicketException("No such Ticket found");
            }
        }


        public async Task UpdateTicketAsync(int ticketId, Ticket ticket)
        {
            Ticket t = await GetTicketByIdAsync(ticketId);
            try
            {
                t.EmpId = ticket.EmpId;
                t.TicketTypeId = ticket.TicketTypeId;
                t.Subject = ticket.Subject;
                t.Description = ticket.Description;
                t.CreatedDate = ticket.CreatedDate;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketException(ex.Message);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            try
            {
                Employee emp = await (from e in ctx.Employees where e.EmpId == empId select e).FirstAsync();
                return emp;
            }
            catch
            {
                throw new TicketException("No such Employee Id");
            }
        }

        public async Task<TicketType> GetTicketTypeByIdAsync(int tyepId)
        {
            try
            {
                TicketType type = await (from t in ctx.TicketTypes where t.TicketTypeId == tyepId select t).FirstAsync();
                return type;
            }
            catch
            {
                throw new TicketException("No such Ticket found..");
            }
        }
    }
}
