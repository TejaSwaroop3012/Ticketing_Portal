using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class TicketFollowUp
    {
        [Display(Name = "Ticket Id")]
        public int TicketId { get; set; }

        [Display(Name = "Serial No")]
        public int SrNo { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = null!;

        [Display(Name = "Updated Date")]
        public DateOnly? UpdatedDate { get; set; }

        [Required(ErrorMessage ="Field is Mandatory")]
        [Display(Name = "Remarks")]
        public string Remarks { get; set; } = null!;
    }
}
