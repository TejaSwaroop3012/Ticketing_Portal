using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketType
    {
        [Display(Name = "TicketType Id")]
        public int TicketTypeId { get; set; }

        [Display(Name = "TicketType Name")]
        public string? TicketTypeName { get; set; }

        [Display(Name = "Assigned To EmpId")]
        public int AssignedToEmpId { get; set; }

        [Display(Name = "TPriority Id")]
        public int PriorityId { get; set; }
    }
}

