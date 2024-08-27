namespace TipMvcApp.Models
{
    public class Employee
    {
        public int EmpId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? EmailId { get; set; }

        public string MobileNo { get; set; } = null!;
    }
}
