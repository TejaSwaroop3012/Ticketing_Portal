using System;
using System.Collections.Generic;

namespace TicketTypeLibrary.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public virtual ICollection<TicketType>? TicketTypes { get; set; } = new List<TicketType>();
}
