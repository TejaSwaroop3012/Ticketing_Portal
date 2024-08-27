namespace TipMvcApp.Models
{
    public class Ticket
    {

        public int TicketId { get; set; }

        public int EmpId { get; set; }

        public int TicketTypeId { get; set; }

        public string Subject { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateOnly? CreatedDate { get; set; }
    }
}
