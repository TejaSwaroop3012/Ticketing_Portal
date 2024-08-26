using System;
using System.Collections.Generic;

namespace TicketPriorityLibrary.Models;

public partial class TicketPriority
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;

    public int RespondWithin { get; set; }

    public int ResolveWithin { get; set; }
}
