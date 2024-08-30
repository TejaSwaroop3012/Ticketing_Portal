using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TicketFollowUpLibrary.Models;
using TicketFollowUpLibrary.Repos;

namespace TicketFollowUpWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketFollowUpController : ControllerBase
    {
        ITicketFollowUpRepoAsync repo;
        public TicketFollowUpController(ITicketFollowUpRepoAsync repository)
        {
            repo = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<TicketFollowup> ticketFollowUps = await repo.GetAllTicketFollowUpsAsync();
            return Ok(ticketFollowUps);
        }

        [HttpGet("{ticketId}/{srNo}")]
        public async Task<ActionResult> Details(int ticketId, int srNo)
        {
            try
            {
                TicketFollowup ticketFollowup = await repo.GetTicketFollowUpAsync(ticketId, srNo);
                return Ok(ticketFollowup);
            }
            catch (TicketFollowUpException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByDate/{updatedDate}")]
        public async Task<ActionResult> ByDate(DateOnly updatedDate)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await repo.GetTicketFollowUpByDateAsync(updatedDate);
                return Ok(ticketFollowups);
            }
            catch (TicketFollowUpException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByStatus/{status}")]
        public async Task<ActionResult> ByStatus(string status)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await repo.GetTicketFollowUpByStatusAsync(status);
                return Ok(ticketFollowups);
            }
            catch (TicketFollowUpException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("ByTicketId/{ticketId}")]
        public async Task<ActionResult> ByTicketId(int ticketId)
        {
            try
            {
                List<TicketFollowup> ticketFollowups = await repo.GetTicketFollowUpByTicketIdAsync(ticketId);
                return Ok(ticketFollowups);
            }
            catch (TicketFollowUpException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(TicketFollowup ticketFollowUp)
        {
            try
            {
                await repo.InsertTicketFollowUpAsync(ticketFollowUp);
                return Created($"api/TicketFollowUp/{ticketFollowUp.TicketId}/{ticketFollowUp.SrNo}", ticketFollowUp);
            }
            catch (TicketFollowUpException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{ticketId}/{srNo}")]
        public async Task<ActionResult> Edit(int ticketId, int srNo, TicketFollowup ticketFollowUp)
        {
            try
            {
                await repo.UpdateTicketFollowUpAsync(ticketId, srNo, ticketFollowUp);
                return Ok(ticketFollowUp);
            }
            catch (TicketFollowUpException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ticketId}/{srNo}")]
        public async Task<ActionResult> Delete(int ticketId, int srNo)
        {
            try
            {
                await repo.DeleteTicketFollowUpAsync(ticketId, srNo);
                return Ok();
            }
            catch (TicketFollowUpException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Ticket")]
        public async Task<ActionResult> InsertTickect(Ticket ticket)
        {
            try
            {
                await repo.AddTicketAsync(ticket);
                return Created($"api/TicketFollowUp/{ticket.TicketId}", ticket);
            }
            catch (TicketFollowUpException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("GetTicket/{ticketId}")]
        public async Task<ActionResult> GetTicket(int ticketId)
        {
            try
            {
                Ticket ticket = await repo.GetTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketFollowUpException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DelTicket/{ticketId}")]
        public async Task<ActionResult> DeleteTicket(int ticketId)
        {
            try
            {
                await repo.DeleteTicketAsync(ticketId);
                return Ok();
            }
            catch (TicketFollowUpException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
