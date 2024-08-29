using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketType
    {
        [Display(Name = "Ticket Type Id")]
        public int TicketTypeId { get; set; }

        [Display(Name = "Ticket Type Name")]
        public string? TicketTypeName { get; set; }

        [Display(Name = "Assigned To EmpId")]
        public int AssignedToEmpId { get; set; }

        [Display(Name = "Priority Id")]
        public int PriorityId { get; set; }
    }
}

