using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketPriority
    {
        [Display(Name = "Priority Id")]
        public int PriorityId { get; set; }

        [MaxLength(30, ErrorMessage = "Priority Name cannot be more than 30 characters")]
        [Display(Name = "Priority Name")]
        public string PriorityName { get; set; } = null!;

        [Display(Name = "Respond Within(hours)")]
        public int RespondWithin { get; set; }

        [Display(Name = "Resolve Within(hours)")]
        public int ResolveWithin { get; set; }
    }
}
