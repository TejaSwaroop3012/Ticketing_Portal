using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketPriority
    {
        [Display(Name = "Priority Id")]
        [Range(1,int.MaxValue,ErrorMessage ="Please enter the Priority Id greater than zero")]
        public int PriorityId { get; set; }

        [MaxLength(30, ErrorMessage = "Priority Name cannot be more than 30 characters")]
        [Display(Name = "Priority Name")]
        public string PriorityName { get; set; } = null!;

        [Display(Name = "Respond Within(hours)")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter the Respond within Hours greater than zero")]
        public int RespondWithin { get; set; }

        [Display(Name = "Resolve Within(hours)")]
        [Range(1, int.MaxValue, ErrorMessage = "please enter the Resolve within Hours greater than zero")]
        public int ResolveWithin { get; set; }
    }
}
