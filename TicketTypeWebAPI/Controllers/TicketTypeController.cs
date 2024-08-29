using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TicketTypeLibrary.Models;
using TicketTypeLibrary.Repos;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;

namespace TicketTypeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketTypeController : ControllerBase
    {
        ITicketTypeRepoAsync ticket;
        public TicketTypeController(ITicketTypeRepoAsync ticketType)
        {
            ticket = ticketType;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {          
                List<TicketType> ticketTypes = await ticket.GetAllTicketType();
                return Ok(ticketTypes);           
        }

        [HttpGet("ByEmployeeId/{EmpId}")]
        public async Task<ActionResult> ByEmployeeId(int EmpId)
        {
            try
            {
                List<TicketType> ticketTypes = await ticket.GetAllbyAssignedEmpId(EmpId);
                return Ok(ticketTypes);
            }
            catch(Exception ex)
            {
                throw new TicketTypeException(ex.Message);
            }
        }
        
        [HttpGet("ByTypeId/{typdId}")]
        public async Task<ActionResult> Details(int typdId)
        {
            try
            {
                TicketType ticketTypes = await ticket.GetTicketbyId(typdId);
                return Ok(ticketTypes);
            }
            catch (TicketTypeException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByEmployee/{empId}")]
        public async Task<ActionResult> DetailsFromEmployee(int empId)
        {
            try
            {
                Employee employee = await ticket.GetEmployeebyId(empId);
                return Ok(employee);
            }
            catch (TicketTypeException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("ByPriority/{priorityId}")]
        public async Task<ActionResult> ByPriority(int priorityId)
        {
            try
            {
                TicketPriority ticketPriority = await ticket.GetTicketPrioritybyId(priorityId);
                return Ok(ticketPriority);
            }
            catch (TicketTypeException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Insert(TicketType ticketType)
        {
            try
            {
                await ticket.InsertTicketType(ticketType);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketSvc/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("TicketType", new {TicketTypeId=ticketType.TicketTypeId});
                return Created($"api/TicketType/{ticketType.TicketTypeId}", ticketType);
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> InsertEmployee(Employee employee)
        {
            try
            {
                await ticket.AddEmployee(employee);
                return Created($"api/TicketType/{employee.EmpId}", employee);
            }
            catch (TicketTypeException ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("TicketPriority")]
        public async Task<ActionResult> InsertPriorityId(TicketPriority ticketPriority)
        {
            try
            {
                await ticket.AddPriority(ticketPriority);
                return Created($"api/TicketType/{ticketPriority.PriorityId}", ticketPriority);
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{TypeId}")]
        public async Task<ActionResult> Update(int TypeId, TicketType type)
        {
            try
            {
                await ticket.UpdateTicketType(TypeId, type);
                return Ok(type);
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("FromTicketType/{TypeId}")]
        public async Task<ActionResult> Delete(int TypeId)
        {
            try
            { 

                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketSvc/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var response = await client2.DeleteAsync($"TicketType/{TypeId}");
                    if (response.IsSuccessStatusCode)
                    {
                        await ticket.DeleteTicketType(TypeId);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Cannot delete");
                    }        
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("FromEmployee/{EmpId}")]
        public async Task<ActionResult> DeleteFromEmployee(int EmpId)
        {
            try
            {
                await ticket.DeleteEmployee(EmpId);
                return Ok();
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("FromPriority/{priorityId}")]
        public async Task<ActionResult> DeleteFromPriority(int priorityId)
        {
            try
            {
                await ticket.DeletePriority(priorityId);
                return Ok();
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
