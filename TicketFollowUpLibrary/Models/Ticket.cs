using System;
using System.Collections.Generic;

namespace TicketFollowUpLibrary.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public virtual ICollection<TicketFollowup>? TicketFollowups { get; set; } = new List<TicketFollowup>();
}
