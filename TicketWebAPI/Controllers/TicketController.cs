using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TicketLibrary.Models;
using TicketLibrary.Repos;

namespace TicketWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        ITicketRepoAsync ticketRepoAsync;
        public TicketController(ITicketRepoAsync repo)
        {
            ticketRepoAsync = repo;
        }

        [HttpGet]
        public async Task<ActionResult> Index1()
        {
            List<Ticket> tickets = await ticketRepoAsync.GetAllTickets();
            return Ok(tickets);
        }
        [HttpGet("GetAllByEmpId/{empId}")]
        public async Task<ActionResult> Index2(int empId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByEmpId(empId);
            return Ok(tickets);
        }
        [HttpGet("GetAllByTypeId/{typeId}")]
        public async Task<ActionResult> Index3(int typeId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByTicketTypeId(typeId);
            return Ok(tickets);
        }
        [HttpGet("GetAllByEmpIdandTypeId/{empId}/{tyepId}")]
        public async Task<ActionResult> Index4(int empId, int tyepId)
        {
            List<Ticket> tickets = await ticketRepoAsync.GetTicketByTicketTypeIdandEmployeeId(empId, tyepId);
            return Ok(tickets);
        }
        [HttpGet("GetEmployeeByEmpId/{empId}")]
        public async Task<ActionResult> Details1(int empId)
        {
            try
            {
                Employee emp = await ticketRepoAsync.GetEmployeeById(empId);
                return Ok(emp);
            }
            catch (TicketException ex)
            {
                return NotFound("No such empid");
            }
        }
        [HttpGet("GetTicketTypeByTicketTypeId/{typeId}")]
        public async Task<ActionResult> Details2(int typeId)
        {
            try
            {
                TicketType type = await ticketRepoAsync.GetTicketTypeById(typeId);
                return Ok(type);
            }
            catch (TicketException ex)
            {
                return NotFound("No such type id");
            }
        }
        [HttpGet("GetTicketByTicketId/{ticketId}")]
        public async Task<ActionResult> Details3(int ticketId)
        {
            try
            {
                Ticket ticket = await ticketRepoAsync.GetTicketById(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound("No such ticket id");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create1(Ticket ticket)
        {
            try
            {
                await ticketRepoAsync.InsertTicket(ticket);
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5076/api/TicketFollowUp/") };
                await client.PostAsJsonAsync("Ticket", new { TicketId = ticket.TicketId});
                return Created($"api/Ticket/{ticket.TicketId}", ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> Create2(Employee employee)
        {
            try
            {
                await ticketRepoAsync.AddEmployee(employee);
                return Created($"api/Ticket/{employee.EmpId}", employee);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TicketType")]
        public async Task<ActionResult> Create3(TicketType type)
        {
            try
            {
                await ticketRepoAsync.AddTicketType(type);
                return Created($"api/Ticket/{type.TicketTypeId}", type);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{ticketId}")]
        public async Task<ActionResult> Edit(int ticketId, Ticket ticket)
        {
            try
            {
                await ticketRepoAsync.UpdateTicket(ticketId, ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> Delete1(int ticketId)
        {
            
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5076/api/TicketFollowUp/") };
                var response1 = await client2.DeleteAsync("DelTicket/" + ticketId);
                if (response1.IsSuccessStatusCode)
                {
                    await ticketRepoAsync.DeleteTicket(ticketId);
                    return Ok();
                }
                else
                {
                    return BadRequest("Cannot delete the Ticket");
                }
        }
        [HttpDelete("Employee/{empId}")]
        public async Task<ActionResult> Delete2(int empId)
        {
            try
            {
                await ticketRepoAsync.DeleteEmployee(empId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("TicketType/{typeId}")]
        public async Task<ActionResult> Delete3(int typeId)
        {
            try
            {
                await ticketRepoAsync.DeleteTicketType(typeId);
                return Ok();
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
