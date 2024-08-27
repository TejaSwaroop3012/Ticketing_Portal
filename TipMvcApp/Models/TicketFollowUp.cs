namespace TipMvcApp.Models
{
    public class TicketFollowUp
    {
        public int TicketId { get; set; }

        public int SrNo { get; set; }

        public string Status { get; set; } = null!;

        public DateOnly? UpdatedDate { get; set; }

        public string Remarks { get; set; } = null!;
    }
}
