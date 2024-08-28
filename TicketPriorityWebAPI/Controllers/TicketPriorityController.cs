using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            List<TicketPriority> ticketPriorities = await ticketPriorityRepo.GetAllTicketPriorities();
            return Ok(ticketPriorities);
        }
        [HttpGet("{priorityId}")]
        public async Task<ActionResult> Details(int priorityId)
        {
            try
            {
                TicketPriority ticketPriority = await ticketPriorityRepo.GetTicketPriorityByPriorityId(priorityId);
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
                await ticketPriorityRepo.InsertTicketPriority(ticketPriority);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                client.DefaultRequestHeaders.Authorization = new
                            System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsJsonAsync("TicketPriority", new { PriorityId = ticketPriority.PriorityId });
                return Created($"api/TicketPriority/{ticketPriority.PriorityId}", ticketPriority);
            }
            catch(TicketPriorityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{priorityId}")]
        public async Task<ActionResult> Edit(int priorityId,TicketPriority ticketPriority)
        {
            try
            {
                await ticketPriorityRepo.UpdateTicketPriority(priorityId, ticketPriority);
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
            try
            {
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                client5.DefaultRequestHeaders.Authorization = new
                             System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1 = await client5.DeleteAsync($"FromPriority/{priorityId}");
                if (response1.IsSuccessStatusCode)
                {
                    await ticketPriorityRepo.DeleteTicketPriority(priorityId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Cannot delete the priorityId");
                }
            }
            catch (TicketPriorityException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
