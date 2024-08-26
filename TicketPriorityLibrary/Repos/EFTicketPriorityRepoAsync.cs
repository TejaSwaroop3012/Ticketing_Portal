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
        public async Task DeleteTicketPriority(int priorityId)
        {
            TicketPriority ticketPriority = await GetTicketPriorityByPriorityId(priorityId);
            try
            {
                ctx.TicketPriorities.Remove(ticketPriority);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new TicketPriorityException(ex.Message);
            }
        }

        public async Task<List<TicketPriority>> GetAllTicketPriorities()
        {
            List<TicketPriority> ticketPriorities = await ctx.TicketPriorities.ToListAsync();
            return ticketPriorities;
        }

        public async Task<TicketPriority> GetTicketPriorityByPriorityId(int priorityId)
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

        public async Task InsertTicketPriority(TicketPriority ticketPriority)
        {
            try
            {
                await ctx.TicketPriorities.AddAsync(ticketPriority);
                await ctx.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new TicketPriorityException(ex.Message);
            }
        }

        public async Task UpdateTicketPriority(int priorityId, TicketPriority ticketPriority)
        {
            try
            {
                TicketPriority ticketPriority2edit = await GetTicketPriorityByPriorityId(priorityId);
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
