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
        Task<List<TicketPriority>> GetAllTicketPriorities();
        Task<TicketPriority> GetTicketPriorityByPriorityId(int priorityId);
        Task InsertTicketPriority(TicketPriority ticketPriority);
        Task UpdateTicketPriority(int priorityId, TicketPriority ticketPriority);
        Task DeleteTicketPriority(int priorityId);
    }
}
