using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;

namespace TipMvcApp.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Ticket> tickets = await client.GetFromJsonAsync<List<Ticket>>("");
            return View(tickets);
        }
        public async Task<ActionResult> ByEmpId(int empId)
        {
            List<Ticket> tickets = await client.GetFromJsonAsync<List<Ticket>>($"GetAllByEmpId/{empId}");
            return View(tickets);
        }
        public async Task<ActionResult> ByTypeId(int typeId)
        {
            List<Ticket> tickets = await client.GetFromJsonAsync<List<Ticket>>($"GetAllByTypeId/{typeId}");
            return View(tickets);
        }
        public async Task<ActionResult> ByEmpIdandTypeId(int empId, int typeId)
        {
            List<Ticket> tickets=await client.GetFromJsonAsync<List<Ticket>>($"GetAllByEmpIdandTypeId/{empId}/{typeId}");
            return View(tickets);
        }
        public async Task<ActionResult> Details(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            try
            {
                await client.PostAsJsonAsync<Ticket>("", ticket);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        

        [Route("Ticket/Edit/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int ticketId)
        {
            Ticket ticket1 = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ticket/Edit/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int ticketId, Ticket ticket)
        {
            try
            {
                await client.PutAsJsonAsync<Ticket>($"{ticketId}", ticket);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Route("Ticket/Delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ticket/Delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int ticketId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{ticketId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
