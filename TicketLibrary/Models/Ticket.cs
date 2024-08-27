using System;
using System.Collections.Generic;

namespace TicketLibrary.Models;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int EmpId { get; set; }

    public int TicketTypeId { get; set; }

    public string Subject { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly? CreatedDate { get; set; }

    public virtual Employee? Emp { get; set; } = null!;

    public virtual TicketType? TicketType { get; set; } = null!;
}
