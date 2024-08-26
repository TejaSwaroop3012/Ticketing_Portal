using System;
using System.Collections.Generic;

namespace TicketTypeLibrary.Models;

public partial class TicketPriority
{
    public int PriorityId { get; set; }

    public virtual ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
}
