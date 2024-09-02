using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketType
    {
        [Required(ErrorMessage = "This field is necessary")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the Ticket Type Id greater than zero")]
        [Display(Name = "Ticket Type Id")]
        public int TicketTypeId { get; set; }

        [Required(ErrorMessage = "This field is necessary")]
        [MaxLength(30, ErrorMessage = "Ticket Type Name cannot be more than 30 characters")]
        [Display(Name = "Ticket Type Name")]
        public string? TicketTypeName { get; set; }

        [Display(Name = "Assigned To EmpId")]
        public int AssignedToEmpId { get; set; }

        [Display(Name = "Priority Id")]
        public int PriorityId { get; set; }
    }
}

