using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketPriorityLibrary.Models;

namespace TicketPriorityLibrary.Repos
{
    public class EFTicketPriorityRepoAsync : ITicketPriorityRepoAsync
    {
        TicketPriorityDBContext ctx = new TicketPriorityDBContext();
        public async Task DeleteTicketPriorityAsync(int priorityId)
        {
            TicketPriority ticketPriority = await GetTicketPriorityByPriorityIdAsync(priorityId);
            try
            {
                ctx.TicketPriorities.Remove(ticketPriority);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new TicketPriorityException(ex.InnerException.Message);
            }
        }

        public async Task<List<TicketPriority>> GetAllTicketPrioritiesAsync()
        {
            List<TicketPriority> ticketPriorities = await ctx.TicketPriorities.ToListAsync();
            return ticketPriorities;
        }

        public async Task<TicketPriority> GetTicketPriorityByPriorityIdAsync(int priorityId)
        {
            try
            {
                TicketPriority ticketpriority = await (from t in ctx.TicketPriorities where t.PriorityId == priorityId select t).FirstAsync();
                return ticketpriority;
            }
            catch
            {
                throw new TicketPriorityException("No such priorityId");
            }
        }

        public async Task InsertTicketPriorityAsync(TicketPriority ticketPriority)
        {
            try
            {
                await ctx.TicketPriorities.AddAsync(ticketPriority);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new TicketPriorityException(ex.InnerException.Message);
            }
        }

        public async Task UpdateTicketPriorityAsync(int priorityId, TicketPriority ticketPriority)
        {
            try
            {
                TicketPriority ticketPriority2edit = await GetTicketPriorityByPriorityIdAsync(priorityId);
                ticketPriority2edit.PriorityName = ticketPriority.PriorityName;
                ticketPriority2edit.RespondWithin = ticketPriority.RespondWithin;
                ticketPriority2edit.ResolveWithin = ticketPriority.ResolveWithin;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketPriorityException(ex.Message);
            }
        }
    }
}
