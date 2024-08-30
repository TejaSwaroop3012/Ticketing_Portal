using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TicketPriorityLibrary.Models;
using TicketPriorityLibrary.Repos;

namespace TicketPriorityWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketPriorityController : ControllerBase
    {
        ITicketPriorityRepoAsync ticketPriorityRepo;
        public TicketPriorityController(ITicketPriorityRepoAsync repo)
        {
            ticketPriorityRepo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<TicketPriority> ticketPriorities = await ticketPriorityRepo.GetAllTicketPrioritiesAsync();
            return Ok(ticketPriorities);
        }
        [HttpGet("{priorityId}")]
        public async Task<ActionResult> Details(int priorityId)
        {
            try
            {
                TicketPriority ticketPriority = await ticketPriorityRepo.GetTicketPriorityByPriorityIdAsync(priorityId);
                return Ok(ticketPriority);
            }
            catch (TicketPriorityException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(TicketPriority ticketPriority)
        {
            try
            {
                await ticketPriorityRepo.InsertTicketPriorityAsync(ticketPriority);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketTypeSvc/") };
                client.DefaultRequestHeaders.Authorization = new
                            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("TicketPriority", new { PriorityId = ticketPriority.PriorityId });
                return Created($"api/TicketPriority/{ticketPriority.PriorityId}", ticketPriority);
            }
            catch(TicketPriorityException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("{priorityId}")]
        public async Task<ActionResult> Edit(int priorityId,TicketPriority ticketPriority)
        {
            try
            {
                await ticketPriorityRepo.UpdateTicketPriorityAsync(priorityId, ticketPriority);
                return Ok(ticketPriority);
            }
            catch(TicketPriorityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{priorityId}")]
        public async Task<ActionResult> Delete(int priorityId)
        {
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketTypeSvc/") };
                client5.DefaultRequestHeaders.Authorization = new
                             System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1 = await client5.DeleteAsync($"FromPriority/{priorityId}");
                if (response1.IsSuccessStatusCode)
                {
                    await ticketPriorityRepo.DeleteTicketPriorityAsync(priorityId);
                    return Ok();
                }
                else
                {
                    string errorMessage = string.Empty;
                    if (!response1.IsSuccessStatusCode)
                    {
                        var errContent = await response1.Content.ReadAsStringAsync();
                        var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                        errorMessage += errorObj.GetProperty("message").GetString() + "\n";
                    }
                    return BadRequest(errorMessage);
                }
            
        }
        
    }
}
