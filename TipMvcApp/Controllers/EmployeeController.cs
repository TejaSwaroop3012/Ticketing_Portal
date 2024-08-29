using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TipMvcApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace TipMvcApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        static HttpClient client = new HttpClient { BaseAddress = new Uri("http://localhost:5216/employeeSvc/") };
        // GET: EmployeeController
        public async Task<ActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            client.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            List<Employee> employees = await client.GetFromJsonAsync<List<Employee>>("");
            empId = employees.Count();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public async Task<ActionResult> Details(int empId)
        {
            try
            {
                Employee employee = await client.GetFromJsonAsync<Employee>($"{empId}");
                return View(employee);
            }
            catch
            {
                return View();
            }
        }
        // GET: EmployeeController/Create
        static int empId;
       [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Employee employee = new Employee();
            employee.EmpId = empId + 1;
            return View(employee);
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View();
            await client.PostAsJsonAsync<Employee>("", employee);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: EmployeeController/Edit/5
        [Route("Employee/Edit/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int empId)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>($"{empId}");
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Employee/Edit/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int empId, Employee employee)
        {
            try
            {
                await client.PutAsJsonAsync<Employee>($"{empId}", employee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        [Route("Employee/Delete/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int empId)
        {
            Employee employee = await client.GetFromJsonAsync<Employee>($"{empId}");
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Employee/Delete/{empId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int empId,Employee employee)
        {
            try
            {
                await client.DeleteAsync($"{empId}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
