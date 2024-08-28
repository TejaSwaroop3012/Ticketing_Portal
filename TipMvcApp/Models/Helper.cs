using Microsoft.AspNetCore.Mvc.Rendering;

namespace TipMvcApp.Models
{
    public class Helper
    {
        static HttpClient clientEmployee = new HttpClient { BaseAddress = new Uri("http://localhost:5292/api/Employee/") };
        static HttpClient clientPriority = new HttpClient { BaseAddress = new Uri("http://localhost:5133/api/TicketPriority/") };
        static HttpClient clientTickeType = new HttpClient { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
        static HttpClient clientTicket = new HttpClient { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
        public static async Task<List<SelectListItem>> GetAllEmployees()
        {
            string userName = "Suresh";
            string role = "admin";
            string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
            string token = await client.GetStringAsync($"{userName}/{role}/{secretKey}");
            clientEmployee.DefaultRequestHeaders.Authorization = new
                   System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<SelectListItem> emplist = new List<SelectListItem>();
            List<Employee> employees = await clientEmployee.GetFromJsonAsync<List<Employee>>("");
            foreach(Employee e in employees)
            {
                emplist.Add(new SelectListItem { Text = $"{e.EmpId}", Value = $"{e.EmpId}" });
            }
            return emplist;
        }

        public static async Task<List<SelectListItem>> GetAllPriorities()
        {
            string userName = "Suresh";
            string role = "admin";
            string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
            string token = await client.GetStringAsync($"{userName}/{role}/{secretKey}");
            clientPriority.DefaultRequestHeaders.Authorization = new
                   System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<SelectListItem> prilist = new List<SelectListItem>();
            List<TicketPriority> ticketPriorities = await clientPriority.GetFromJsonAsync<List<TicketPriority>>("");
            foreach (TicketPriority p in ticketPriorities)
            {
                prilist.Add(new SelectListItem { Text = $"{p.PriorityId}", Value = $"{p.PriorityId}" });
            }
            return prilist;
        }

        public static async Task<List<SelectListItem>> GetAllTicketTypes()
        {
            string userName = "Suresh";
            string role = "admin";
            string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
            string token = await client.GetStringAsync($"{userName}/{role}/{secretKey}");
            clientTickeType.DefaultRequestHeaders.Authorization = new
                   System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<SelectListItem> ttypelist = new List<SelectListItem>();
            List<TicketType> ticketTypes = await clientTickeType.GetFromJsonAsync<List<TicketType>>("");
            foreach (TicketType ty in ticketTypes)
            {
                ttypelist.Add(new SelectListItem { Text = $"{ty.TicketTypeId}", Value = $"{ty.TicketTypeId}" });
            }
            return ttypelist;
        }
        public static async Task<List<SelectListItem>> GetAllTickets()
        {
            string userName = "Suresh";
            string role = "admin";
            string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
            HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
            string token = await client.GetStringAsync($"{userName}/{role}/{secretKey}");
            clientTickeType.DefaultRequestHeaders.Authorization = new
                   System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<SelectListItem> ticketlist = new List<SelectListItem>();
            List<Ticket> ticket = await clientTicket.GetFromJsonAsync<List<Ticket>>("");
            foreach (Ticket t in ticket)
            {
                ticketlist.Add(new SelectListItem { Text = $"{t.TicketId}", Value = $"{t.TicketId}" });
            }
            return ticketlist;
        }
    }
}
