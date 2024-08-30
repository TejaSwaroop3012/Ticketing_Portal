using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class Ticket
    {
        [Display (Name="Ticket Id")]
        public int TicketId { get; set; }
        [Display(Name ="Employee Id")]
        [Required(ErrorMessage = "This field is necessary")]
        public int EmpId { get; set; }
        [Display(Name ="Ticket Type Id")]
        [Required(ErrorMessage = "This field is necessary")]
        public int TicketTypeId { get; set; }
        [Display(Name ="Subject")]
        [MaxLength(50,ErrorMessage ="Subject must be less than 50 character")]
        public string Subject { get; set; } = null!;
        [Display(Name ="Description")]
        [MaxLength(100, ErrorMessage = "Description must be less than 100 character")]
        public string Description { get; set; } = null!;
        
        public DateOnly? CreatedDate { get; set; }
    }
}
