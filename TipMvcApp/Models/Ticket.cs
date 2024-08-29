using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TipMvcApp.Models
{
    public class Ticket
    {
        [Display (Name="Ticket Id")]
        public int TicketId { get; set; }
        [Display(Name ="Employee Id")]
        public int EmpId { get; set; }
        [Display(Name ="Ticket Type Id")]
        public int TicketTypeId { get; set; }
        [Display(Name ="Subject")]
        public string Subject { get; set; } = null!;
        [Display(Name ="Description")]
        public string Description { get; set; } = null!;
        
        public DateOnly? CreatedDate { get; set; }
    }
}
