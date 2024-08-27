using System;
using System.Collections.Generic;

namespace TicketFollowUpLibrary.Models;

public partial class TicketFollowup
{
    public int TicketId { get; set; }

    public int SrNo { get; set; }

    public string Status { get; set; } = null!;

    public DateOnly? UpdatedDate { get; set; }

    public string Remarks { get; set; } = null!;

    public virtual Ticket? Ticket { get; set; } = null!;
}
