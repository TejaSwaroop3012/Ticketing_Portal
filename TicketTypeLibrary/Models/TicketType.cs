using System;
using System.Collections.Generic;

namespace TicketTypeLibrary.Models;

public partial class TicketType
{
    public int TicketTypeId { get; set; }

    public string? TicketTypeName { get; set; }

    public int AssignedToEmpId { get; set; }

    public int PriorityId { get; set; }

    public virtual Employee AssignedToEmp { get; set; } = null!;

    public virtual TicketPriority Priority { get; set; } = null!;
}
