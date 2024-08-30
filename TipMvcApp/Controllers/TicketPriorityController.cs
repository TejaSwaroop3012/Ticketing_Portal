using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Azure;

namespace TipMvcApp.Controllers
{
    [Authorize]
    public class TicketPriorityController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketPrioritySvc/") };
        // GET: TicketPriorityController
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<TicketPriority> ticketPriorities = await client.GetFromJsonAsync<List<TicketPriority>>("");
            return View(ticketPriorities);
        }
        // GET: TicketPriorityController/Details/5
        public async Task<ActionResult> Details(int priorityId)
        {
            TicketPriority ticketPriority = await client.GetFromJsonAsync<TicketPriority>($"{priorityId}");
            return View(ticketPriority);
        }
        // GET: TicketPriorityController/Create
 
       [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        // POST: TicketPriorityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(TicketPriority ticketPriority)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
                var response = await client.PostAsJsonAsync<TicketPriority>("", ticketPriority);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorObj = JsonSerializer.Deserialize<JsonElement>(errorContent);
                string errorMessage = errorObj.GetProperty("message").GetString();

                throw new Exception(errorMessage);
            }
           
        }
        // GET: TicketPriorityController/Edit/5
        [Route("TicketPriority/Edit/{priorityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int priorityId)
        {
            TicketPriority ticketPriority = await client.GetFromJsonAsync<TicketPriority>($"{priorityId}");
            return View(ticketPriority);
        }
        // POST: TicketPriorityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketPriority/Edit/{priorityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int priorityId, TicketPriority ticketPriority)
        {
            try
            {
                await client.PutAsJsonAsync<TicketPriority>($"{priorityId}", ticketPriority);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketPriorityController/Delete/5
        [Route("TicketPriority/Delete/{priorityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int priorityId)
        {
            TicketPriority ticketPriority = await client.GetFromJsonAsync<TicketPriority>(""+ priorityId);
            return View(ticketPriority);
        }
        // POST: TicketPriorityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketPriority/Delete/{priorityId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int priorityId, IFormCollection collection)
        {
            var response = await client.DeleteAsync($"{priorityId}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }
    }
}
