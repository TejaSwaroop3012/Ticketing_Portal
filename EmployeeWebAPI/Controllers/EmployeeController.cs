using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Azure;


namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepoAsync employeeRepository;
        public EmployeeController(IEmployeeRepoAsync repoAsync)
        {
            employeeRepository = repoAsync;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<Employee> employees = await employeeRepository.GetAllEmployees();
            return Ok(employees);
        }
        [HttpGet("{empId}")]
        public async Task<ActionResult> Details(int empId)
        {
            try
            {
                Employee employee = await employeeRepository.GetByEmpId(empId);
                return Ok(employee);
            }
            catch (EmployeeException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                await employeeRepository.InsertEmployee(employee);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
                client3.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client3.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
                return Created($"api/Employee/{employee.EmpId}", employee);
            }
            catch(EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{empId}")]
        public async Task<ActionResult> Edit(int empId,Employee employee)
        {
            try
            {
                await employeeRepository.UpdateEmployee(empId,employee);
                return Ok(employee);
            }
            catch(EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{empId}")]
        public async Task<ActionResult> Delete(int empId)
        {
            try
            {
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5153/api/Auth/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1=await client2.DeleteAsync("FromEmployee/" + empId );
                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response2=await client3.DeleteAsync("Employee/" + empId );
                if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {
                    await employeeRepository.DeleteEmployee(empId);
                    return Ok();
                }
                else
                {
                    if (response1.IsSuccessStatusCode)
                    {
                        HttpClient client4 = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                        client4.DefaultRequestHeaders.Authorization = new 
                                        System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client4.PostAsJsonAsync("Employee/", new {EmpId = empId});
                    }
                    if (response2.IsSuccessStatusCode)
                    {
                        HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
                        client5.DefaultRequestHeaders.Authorization = new
                                        System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client5.PostAsJsonAsync("Employee/", new { EmpId = empId });
                    }
                    return BadRequest("Cannot delete the Employee");
                }
            }
            catch(EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
