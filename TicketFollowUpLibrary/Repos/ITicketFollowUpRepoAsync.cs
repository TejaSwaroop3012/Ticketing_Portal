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
        Task<List<TicketFollowup>> GetAllTicketFollowUps();
        Task<List<TicketFollowup>> GetTicketFollowUpByTicketId(int ticketId);
        Task<List<TicketFollowup>> GetTicketFollowUpByStatus(string status);
        Task<List<TicketFollowup>> GetTicketFollowUpByDate(DateOnly updatedDate);
        Task<TicketFollowup> GetTicketFollowUp(int ticketId, int srNo);
        Task InsertTicketFollowUp(TicketFollowup ticketFollowUp);
        Task UpdateTicketFollowUp(int ticketId, int srNo, TicketFollowup ticketFollowUp);
        Task DeleteTicketFollowUp(int ticketId, int srNo);
        Task AddTicket(Ticket ticket);
        Task<Ticket> GetTicket(int ticketId);
        Task DeleteTicket(int ticketId);

    }
}
