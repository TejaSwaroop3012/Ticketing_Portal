using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTypeLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace TicketTypeLibrary.Repos
{
    public class EFTicketTypeRepoAsync : ITicketTypeRepoAsync
    {
        TicketTypeDBContext ctx = new TicketTypeDBContext();
        public async Task AddEmployee(Employee Emp)
        {
            try
            {
                await ctx.Employees.AddAsync(Emp);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }

        public async Task AddPriority(TicketPriority priority)
        {
            try
            {
                await ctx.TicketPriorities.AddAsync(priority);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }
        public async Task<Employee> GetEmployeebyId(int empId)
        {
            try
            {
                Employee employee = await (from emp in ctx.Employees where emp.EmpId == empId select emp).FirstAsync();
                return employee;
            }
            catch
            {
                throw new TicketTypeException("No such Employee");
            }
            
        }

        public async Task DeleteEmployee(int EmpId)
        {
            try
            {
                Employee employee = await GetEmployeebyId(EmpId);
                ctx.Employees.Remove(employee);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }

        public async Task<TicketPriority> GetTicketPrioritybyId(int priorityId)
        {
            try
            {
                TicketPriority ticketPriority = await (from tp in ctx.TicketPriorities where tp.PriorityId == priorityId select tp).FirstAsync();
                return ticketPriority;
            }
            catch
            {
                throw new TicketTypeException("No such PriorityId");
            }
        }

        public async Task DeletePriority(int priorityId)
        {
            try
            {
                TicketPriority ticketPriority = await GetTicketPrioritybyId(priorityId);
                ctx.TicketPriorities.Remove(ticketPriority);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }

        public async Task DeleteTicketType(int TypeId)
        {
            try
            {
                TicketType ticketType = await GetTicketbyId(TypeId);
                ctx.TicketTypes.Remove(ticketType);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }

        public async Task<List<TicketType>> GetAllbyAssignedEmpId(int EmpId)
        {
            List<TicketType> ticketTypes = await (from t in ctx.TicketTypes where t.AssignedToEmpId == EmpId select t).ToListAsync();
            if (ticketTypes.Count > 0)
            {
                return ticketTypes;
            }
            else
            {
                throw new TicketTypeException("No such Employees");
            }
        }

        public async Task<List<TicketType>> GetAllTicketType()
        {
            List<TicketType> ticketTypes = await ctx.TicketTypes.ToListAsync();
            return ticketTypes;
        }
        public async Task<TicketType> GetTicketbyId(int TypeId)
        {
            try
            {
                TicketType ticketType = await (from ty in ctx.TicketTypes where ty.TicketTypeId == TypeId select ty).FirstAsync();
                return ticketType;
            }
            catch
            {
                throw new TicketTypeException("No such ticketId");
            }
        }
        public async Task InsertTicketType(TicketType type)
        {
            try
            {
                await ctx.TicketTypes.AddAsync(type);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }

        public async Task UpdateTicketType(int TypeId, TicketType type)
        {
            try
            {
                TicketType ticketType = await GetTicketbyId(TypeId);
                ticketType.TicketTypeName = type.TicketTypeName;
                ticketType.AssignedToEmpId = type.AssignedToEmpId;
                ticketType.PriorityId = type.PriorityId;
                await ctx.SaveChangesAsync();
            }
            catch
            {
                throw new TicketTypeException("Cannot Update");
            }

        }
    }
}
