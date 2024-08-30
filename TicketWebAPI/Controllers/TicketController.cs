using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net.Sockets;
using System.Text.Json;
using TicketLibrary.Models;
using TicketLibrary.Repos;

namespace TicketWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {

        ITicketRepoAsync ticketRepoAsync;
        public TicketController(ITicketRepoAsync repo)
        {
            ticketRepoAsync = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Ticket> tickets = await ticketRepoAsync.GetAllTicketsAsync();
            return Ok(tickets);
        }
        [HttpGet("GetAllByEmpId/{empId}")]
        public async Task<ActionResult> GetAllByEmpId(int empId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByEmpIdAsync(empId);
            return Ok(tickets);
        }
        [HttpGet("GetAllByTypeId/{typeId}")]
        public async Task<ActionResult> GetAllByTypeId(int typeId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByTicketTypeIdAsync(typeId);
            return Ok(tickets);
        }
        [HttpGet("GetAllByEmpIdandTypeId/{empId}/{tyepId}")]
        public async Task<ActionResult> GetAllByEmpIdandTypeId(int empId, int tyepId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByTicketTypeIdandEmployeeIdAsync(empId, tyepId);
            return Ok(tickets);
        }
        [HttpGet("GetTicketByTicketId/{ticketId}")]
        public async Task<ActionResult> Details(int ticketId)
        {
            try
            {
                Ticket ticket = await ticketRepoAsync.GetTicketByIdAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound("No such ticket id");
            }
        }
        [HttpGet("GetEmployeeByEmpId/{empId}")]
        public async Task<ActionResult> GetEmployeeByEmpId(int empId)
        {
            try
            {
                Employee emp = await ticketRepoAsync.GetEmployeeByIdAsync(empId);
                return Ok(emp);
            }
            catch (TicketException ex)
            {
                return NotFound("No such empid");
            }
        }
        [HttpGet("GetTicketTypeByTicketTypeId/{typeId}")]
        public async Task<ActionResult> GetTicketTypeByTicketTypeId(int typeId)
        {
            try
            {
                TicketType type = await ticketRepoAsync.GetTicketTypeByIdAsync(typeId);
                return Ok(type);
            }
            catch (TicketException ex)
            {
                return NotFound("No such type id");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            try
            {
                await ticketRepoAsync.AddTicketAsync(ticket);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketFollowUpSvc/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("Ticket", new { TicketId = ticket.TicketId });
                return Created($"api/Ticket/{ticket.TicketId}", ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            try
            {
                await ticketRepoAsync.AddEmployeeAsync(employee);
                return Created($"api/Ticket/{employee.EmpId}", employee);
            }
            catch (TicketException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("TicketType")]
        public async Task<ActionResult> CreateTicketType(TicketType type)
        {
            try
            {
                await ticketRepoAsync.AddTicketTypeAsync(type);
                return Created($"api/Ticket/{type.TicketTypeId}", type);
            }
            catch (TicketException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPut("{ticketId}")]
        public async Task<ActionResult> Edit(int ticketId, Ticket ticket)
        {
            try
            {
                await ticketRepoAsync.UpdateTicketAsync(ticketId, ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> Delete(int ticketId)
        {
            string userName = "Suresh";
            string role = "admin";
            string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
            HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
            string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
            HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketFollowUpSvc/") };
            client2.DefaultRequestHeaders.Authorization = new
                System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1 = await client2.DeleteAsync("DelTicket/" + ticketId);
                if (response1.IsSuccessStatusCode)
                {
                    await ticketRepoAsync.DeleteTicketAsync(ticketId);
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
        [HttpDelete("Employee/{empId}")]
        public async Task<ActionResult> DeleteEmployee(int empId)
        {
            try
            {
                await ticketRepoAsync.DeleteEmployeeAsync(empId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpDelete("TicketType/{typeId}")]
        public async Task<ActionResult> DeleteTicketType(int typeId)
        {
            try
            {
                await ticketRepoAsync.DeleteTicketTypeAsync(typeId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

