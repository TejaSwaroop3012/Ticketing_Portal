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
        // GET: TicketController
        public async Task<ActionResult> Index()
        {
            List<Ticket> tickets = await client.GetFromJsonAsync <List<Ticket>>("");
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return View(tickets);
        }
        // GET: TicketController/Details/5
        public async Task<ActionResult> Details(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }
        // GET: TicketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        

        // GET: TicketController/Edit/5
        [Route("Ticket/Edit/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int ticketId)
        {
            Ticket ticket1 = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket1);
        }

        // POST: TicketController/Edit/5
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


        // GET: TicketController/Delete/5
        [Route("Ticket/Delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }

        // POST: TicketController/Delete/5
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
