using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TipMvcApp.Models;


namespace TipMvcApp.Controllers
{
    [Authorize]
    public class TicketTypeController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri(" http://localhost:5026/ticketTypeSvc/") };
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<TicketType> ticketTypes = await client.GetFromJsonAsync<List<TicketType>>("");
            return View(ticketTypes);
        }
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(TicketType ticketType)
        {
            if (!ModelState.IsValid)
                return View();
            var response= await client.PostAsJsonAsync<TicketType>("", ticketType);
            if(response.IsSuccessStatusCode)
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
        [Route("TicketType/Edit/{TypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int TypeId)
        {
            TicketType ticketType = await client.GetFromJsonAsync<TicketType>($"ByTypeId/{TypeId}");
            return View(ticketType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketType/Edit/{TypeId}")]
        [Authorize(Roles = "Admin")]
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
        [Route("TicketType/Delete/{TypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int TypeId)
        {
            TicketType ticketType = await client.GetFromJsonAsync<TicketType>("ByTypeId/" + TypeId);
            return View(ticketType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("TicketType/Delete/{TypeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int TypeId, IFormCollection collection)
        {       
             var response = await client.DeleteAsync($"FromTicketType/{TypeId}");
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
