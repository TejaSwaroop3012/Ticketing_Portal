using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TicketTypeLibrary.Models;
using TicketTypeLibrary.Repos;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TicketTypeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypeController : ControllerBase
    {
        ITicketTypeRepoAsync ticket;
        public TicketTypeController(ITicketTypeRepoAsync ticketType)
        {
            ticket = ticketType;
        }
        [HttpGet]
        public async Task<ActionResult> Index1()
        {          
                List<TicketType> ticketTypes = await ticket.GetAllTicketType();
                return Ok(ticketTypes);           
        }

        [HttpGet("ByEmployeeId/{EmpId}")]
        public async Task<ActionResult> Index2(int EmpId)
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
        public async Task<ActionResult> Details1(int typdId)
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
        public async Task<ActionResult> Details2(int empId)
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
        public async Task<ActionResult> Details3(int priorityId)
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
        public async Task<ActionResult> Insert1(TicketType ticketType)
        {
            try
            {
                await ticket.InsertTicketType(ticketType);
                return Created($"api/TicketType/{ticketType.TicketTypeId}", ticketType);
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Employee")]
        public async Task<ActionResult> Insert2(Employee employee)
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
        public async Task<ActionResult> Insert3(TicketPriority ticketPriority)
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
        public async Task<ActionResult> Delete1(int TypeId)
        {
            try
            {
                await ticket.DeleteTicketType(TypeId);
                return Ok();
            }
            catch (TicketTypeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("FromEmployee/{EmpId}")]
        public async Task<ActionResult> Delete2(int EmpId)
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
        public async Task<ActionResult> Delete3(int priorityId)
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
