using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketPriorityLibrary.Models;

namespace TicketPriorityLibrary.Repos
{
    public interface ITicketPriorityRepoAsync
    {
        Task<List<TicketPriority>> GetAllTicketPrioritiesAsync();
        Task<TicketPriority> GetTicketPriorityByPriorityIdAsync(int priorityId);
        Task InsertTicketPriorityAsync(TicketPriority ticketPriority);
        Task UpdateTicketPriorityAsync(int priorityId, TicketPriority ticketPriority);
        Task DeleteTicketPriorityAsync(int priorityId);
    }
}
