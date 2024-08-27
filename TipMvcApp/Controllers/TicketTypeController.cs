using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;


namespace TipMvcApp.Controllers
{
    public class TicketTypeController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:5031/api/TicketType/") };
        // GET: TicketTypeController
        public async Task<ActionResult> Index()
        {
            List<TicketType> ticketTypes = await client.GetFromJsonAsync<List<TicketType>>("");
            return View(ticketTypes);
        }

        // GET: TicketTypeController/Details/5
        public async Task<ActionResult> Details(int TypeId)
        {
            TicketType ticketType = await client.GetFromJsonAsync<TicketType>($"ByTypeId/{TypeId}");
            return View(ticketType);
        }

        public async Task<ActionResult> ByAssignedEmpId(int EmpId)
        {
            List<TicketType> ticketTypes = await client.GetFromJsonAsync<List<TicketType>>($"ByEmployeeId/{EmpId}");
            return View(ticketTypes);
        }

        // GET: TicketTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TicketType ticketType)
        {
            try
            {
                await client.PostAsJsonAsync<TicketType>("", ticketType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketTypeController/Edit/5
        [Route("TicketType/Edit/{TypeId}")]
        public async Task<ActionResult> Edit(int TypeId)
        {
            TicketType ticketType = await client.GetFromJsonAsync<TicketType>($"ByTypeId/{TypeId}");
            return View(ticketType);
        }

        // POST: TicketTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketType/Edit/{TypeId}")]
        public async Task<ActionResult> Edit(int TypeId, TicketType ticketType)
        {
            try
            {
                await client.PutAsJsonAsync<TicketType>($"{TypeId}", ticketType);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketTypeController/Delete/5
        [Route("TicketType/Delete/{TypeId}")]
        public async Task<ActionResult> Delete(int TypeId)
        {
            TicketType ticketType = await client.GetFromJsonAsync<TicketType>("ByTypeId/" + TypeId);
            return View(ticketType);
        }

        // POST: TicketTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketType/Delete/{TypeId}")]
        public async Task<ActionResult> Delete(int TypeId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"FromTicketType/{TypeId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
