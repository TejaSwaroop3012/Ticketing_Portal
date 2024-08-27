using System;
using System.Collections.Generic;

namespace TicketLibrary.Models;

public partial class TicketType
{
    public int TicketTypeId { get; set; }

    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
