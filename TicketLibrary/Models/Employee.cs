using System;
using System.Collections.Generic;

namespace TicketLibrary.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public virtual ICollection<Ticket>? Tickets { get; set; } = new List<Ticket>();
}
