using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketFollowUpLibrary.Models;

namespace TicketFollowUpLibrary.Repos
{
    public interface ITicketFollowUpRepoAsync
    {
        Task<List<TicketFollowup>> GetAllTicketFollowUpsAsync();
        Task<List<TicketFollowup>> GetTicketFollowUpByTicketIdAsync(int ticketId);
        Task<List<TicketFollowup>> GetTicketFollowUpByStatusAsync(string status);
        Task<List<TicketFollowup>> GetTicketFollowUpByDateAsync(DateOnly updatedDate);
        Task<TicketFollowup> GetTicketFollowUpAsync(int ticketId, int srNo);
        Task InsertTicketFollowUpAsync(TicketFollowup ticketFollowUp);
        Task UpdateTicketFollowUpAsync(int ticketId, int srNo, TicketFollowup ticketFollowUp);
        Task DeleteTicketFollowUpAsync(int ticketId, int srNo);
        Task AddTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketAsync(int ticketId);
        Task DeleteTicketAsync(int ticketId);

    }
}
