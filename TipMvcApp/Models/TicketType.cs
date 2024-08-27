namespace TipMvcApp.Models
{
    public class TicketType
    {
        public int TicketTypeId { get; set; }

        public string? TicketTypeName { get; set; }

        public int AssignedToEmpId { get; set; }

        public int PriorityId { get; set; }
    }
}
