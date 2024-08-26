using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;

namespace TipMvcApp.Controllers
{
    [Authorize]
    public class TicketFollowUpController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:5076/api/TicketFollowUp/") };
        // GET: TicketFollowUpController
        public async Task<ActionResult> Index()
        {
            List<TicketFollowUp> ticketFollowUps = await client.GetFromJsonAsync<List<TicketFollowUp>>("");
            return View(ticketFollowUps);
        }
        public async Task<ActionResult> ByStatus(string status)
        {
            List<TicketFollowUp> ticketFollowUps = await client.GetFromJsonAsync<List<TicketFollowUp>>($"ByStatus/{status}");
            return View(ticketFollowUps);
        }
        public async Task<ActionResult> ByDate(DateOnly updatedDate)
        {
            var date = updatedDate.ToString("yyyy-MM-dd");
            List<TicketFollowUp> ticketFollowUps = await client.GetFromJsonAsync<List<TicketFollowUp>>($"ByDate/{date}");
            return View(ticketFollowUps);
        }
        public async Task<ActionResult> ByTicketId(int ticketId)
        {
            List<TicketFollowUp> ticketFollowUps = await client.GetFromJsonAsync<List<TicketFollowUp>>($"ByTicketId/{ticketId}");
            return View(ticketFollowUps);
        }

        // GET: TicketFollowUpController/Details/5
        public async Task<ActionResult> Details(int ticketId, int srNo)
        {
            TicketFollowUp ticketFollowUp = await client.GetFromJsonAsync<TicketFollowUp>($"{ticketId}/{srNo}");
            return View(ticketFollowUp);
        }

        // GET: TicketFollowUpController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketFollowUpController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TicketFollowUp ticketFollowUp)
        {
            try
            {
                await client.PostAsJsonAsync<TicketFollowUp>("",ticketFollowUp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketFollowUpController/Edit/5
        [Authorize (Roles = "Admin")]
        [Route("TicketFollowUp/Edit/{ticketId}/{srNo}")]
        public async Task<ActionResult> Edit(int ticketId, int srNo)
        {
            TicketFollowUp ticketFollowUp = await client.GetFromJsonAsync<TicketFollowUp>($"{ticketId}/{srNo}");
            return View(ticketFollowUp);
        }

        // POST: TicketFollowUpController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketFollowUp/Edit/{ticketId}/{srNo}")]
        public async Task<ActionResult> Edit(int ticketId, int srNo, TicketFollowUp ticketFollowUp)
        {
            try
            {
                await client.PutAsJsonAsync<TicketFollowUp>($"{ticketId}/{srNo}",ticketFollowUp);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketFollowUpController/Delete/5
        [Route("TicketFollowUp/Delete/{ticketId}/{srNo}")]
        public async Task<ActionResult> Delete(int ticketId, int srNo)
        {
            TicketFollowUp ticketFollowUp = await client.GetFromJsonAsync<TicketFollowUp>($"{ticketId}/{srNo}");
            return View(ticketFollowUp);
        }

        // POST: TicketFollowUpController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketFollowUp/Delete/{ticketId}/{srNo}")]
        public async Task<ActionResult> Delete(int ticketId, int srNo, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{ticketId}/{srNo}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
