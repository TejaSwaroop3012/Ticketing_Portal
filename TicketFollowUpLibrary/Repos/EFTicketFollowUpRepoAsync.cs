using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketFollowUpLibrary.Models;

namespace TicketFollowUpLibrary.Repos
{
    public class EFTicketFollowUpRepoAsync : ITicketFollowUpRepoAsync
    {
        TicketFollowUpDBContext ctx = new TicketFollowUpDBContext();
        public async Task AddTicket(Ticket ticket)
        {
            try
            {
                await ctx.Tickets.AddAsync(ticket);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketFollowUpException(ex.Message);
            }
        }

        public async Task<Ticket> GetTicket(int ticketId)
        {
            try
            {
                Ticket ticket = await (from t in ctx.Tickets where t.TicketId == ticketId select t).FirstAsync();
                return ticket;
            }
            catch
            {
                throw new TicketFollowUpException("No such Ticket Id");
            }
        }

        public async Task DeleteTicket(int ticketId)
        {
            try
            {
                Ticket t2del = await GetTicket(ticketId);
                ctx.Tickets.Remove(t2del);
            }
            catch (Exception ex)
            {
                throw new TicketFollowUpException(ex.Message);
            }
        }

        public async Task DeleteTicketFollowUp(int ticketId, int srNo)
        {
            try
            {
                TicketFollowup tf2del = await GetTicketFollowUp(ticketId, srNo);
                ctx.TicketFollowups.Remove(tf2del);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketFollowUpException(ex.Message);
            }
        }

        public async Task<List<TicketFollowup>> GetAllTicketFollowUps()
        {
            List<TicketFollowup> ticketFollowups = await ctx.TicketFollowups.ToListAsync();
            return ticketFollowups;
        }

        public async Task<TicketFollowup> GetTicketFollowUp(int ticketId, int srNo)
        {
            try
            {
                TicketFollowup ticketFollowup = await (from tf in ctx.TicketFollowups where tf.TicketId == ticketId && tf.SrNo == srNo select tf).FirstAsync();
                return ticketFollowup;
            }
            catch
            {
                throw new TicketFollowUpException("No such Ticket Number OR Serial Number");
            }
        }

        public async Task<List<TicketFollowup>> GetTicketFollowUpByDate(DateOnly updatedDate)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await (from tf in ctx.TicketFollowups where tf.UpdatedDate == updatedDate select tf).ToListAsync();
                return ticketFollowups;
            }
            catch
            {
                throw new TicketFollowUpException("No Follow Ups On that Date");
            }
        }

        public async Task<List<TicketFollowup>> GetTicketFollowUpByStatus(string status)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await (from tf in ctx.TicketFollowups where tf.Status == status select tf).ToListAsync();
                return ticketFollowups;
            }
            catch
            {
                throw new TicketFollowUpException("No Such Status in the Follow Ups");
            }
        }

        public async Task<List<TicketFollowup>> GetTicketFollowUpByTicketId(int ticketId)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await (from tf in ctx.TicketFollowups where tf.TicketId == ticketId select tf).ToListAsync();
                return ticketFollowups;
            }
            catch
            {
                throw new TicketFollowUpException("No Such Ticket ID in the Follow Ups");
            }
        }

        public async Task InsertTicketFollowUp(TicketFollowup ticketFollowUp)
        {
            try
            {
                await ctx.TicketFollowups.AddAsync(ticketFollowUp);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketFollowUpException(ex.Message);
            }
        }

        public async Task UpdateTicketFollowUp(int ticketId, int srNo, TicketFollowup ticketFollowUp)
        {
            try
            {
                TicketFollowup tf2edit = await GetTicketFollowUp(ticketId, srNo);
                tf2edit.Status = ticketFollowUp.Status;
                tf2edit.Remarks = ticketFollowUp.Remarks;
                tf2edit.UpdatedDate = ticketFollowUp.UpdatedDate;
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new TicketFollowUpException(ex.Message);
            }
        }
    }
}
