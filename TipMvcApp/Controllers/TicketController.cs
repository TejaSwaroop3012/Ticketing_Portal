using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;

namespace TipMvcApp.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        static HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
        // GET: TicketController
        public async Task<ActionResult> Index()
        {
            List<Ticket> tickets = await client.GetFromJsonAsync <List<Ticket>>("");
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return View(tickets);
        }
        //public async Task<ActionResult> GetAllByEmpId(int empId)
        //{
        //    List<Ticket> tickets = await client.GetFromJsonAsync<List<Ticket>>($"GetAllByEmpId/{empId}");
        //    return View(tickets);
        //}
        //public async Task<ActionResult> GetAllByTypeId(int typeId)
        //{
        //    List<Ticket> tickets=await client.GetFromJsonAsync<List<Ticket>>($"GetAllByTypeId/{typeId}");
        //    return View(tickets);
        //}
        //public async Task<ActionResult> GetAllByEmpIdandTypeId(int empId,int typeId)
        //{
        //    List<Ticket> tickets=await client.GetFromJsonAsync<List<Ticket>>($"GetAllByEmpIdandTypeId/{empId}/{typeId}");
        //    return View(tickets);
        //}

        // GET: TicketController/Details/5
        //public async Task<ActionResult> GetEmployeeByEmpId(int empId)
        //{
        //    Employee emp = await client.GetFromJsonAsync<Employee>($"GetEmployeeByEmpId/{empId}");
        //    return View(emp);
        //}
        //public async Task<ActionResult> GetTicketTypeByTicketTypeId(int typeId)
        //{
        //    TicketType type = await client.GetFromJsonAsync<TicketType>($"GetTicketTypeByTicketTypeId/{typeId}");
        //    return View(type);
        //}
        public async Task<ActionResult> Details(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }
        // GET: TicketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ticket ticket)
        {
            try
            {
                await client.PostAsJsonAsync<Ticket>("", ticket);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: TicketController/CreateEmp
        //public ActionResult CreateEmployee()
        //{
        //    return View();
        //}

        //// POST: TicketController/CreateEmp
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateEmployee(Employee emp)
        //{
        //    try
        //    {
        //        await client.PostAsJsonAsync<Employee>($"Employee", emp);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //// GET: TicketController/Create
        //public ActionResult CreateTicketType()
        //{
        //    return View();
        //}

        //// POST: TicketController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateTicketType(TicketType type)
        //{
        //    try
        //    {
        //        await client.PostAsJsonAsync<TicketType>($"TicketType", type);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: TicketController/Edit/5
        [Route("Ticket/Edit/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int ticketId)
        {
            Ticket ticket1 = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket1);
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ticket/Edit/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int ticketId, Ticket ticket)
        {
            try
            {
                await client.PutAsJsonAsync<Ticket>($"{ticketId}", ticket);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: TicketController/Delete/5
        [Route("Ticket/Delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int ticketId)
        {
            Ticket ticket = await client.GetFromJsonAsync<Ticket>($"GetTicketByTicketId/{ticketId}");
            return View(ticket);
        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Ticket/Delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int ticketId, IFormCollection collection)
        {
            try
            {
                await client.DeleteAsync($"{ticketId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //public async Task<ActionResult> DeleteEmployee(int empId)
        //{
        //    Employee emp = await client.GetFromJsonAsync<Employee>($"GetEmployeeByEmpId/{empId}");
        //    return View(emp);
        //}

        //// POST: TicketController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteEmployee(int empId, IFormCollection collection)
        //{
        //    try
        //    {
        //        await client.DeleteAsync($"{empId}");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //public async Task<ActionResult> DeleteTicketType(int typeId)
        //{
        //    TicketType ty = await client.GetFromJsonAsync<TicketType>($"GetTicketTypeByTicketTypeId/{typeId}");
        //    return View(ty);
        //}

        //// POST: TicketController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteTicketType(int typeId, IFormCollection collection)
        //{
        //    try
        //    {
        //        await client.DeleteAsync($"{typeId}");
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

    }
}
