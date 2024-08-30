using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Azure;
using System.Text.Json;


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
            List<Employee> employees = await employeeRepository.GetAllEmployeesAsync();
            return Ok(employees);
        }
        [HttpGet("{empId}")]
        public async Task<ActionResult> Details(int empId)
        {
            try
            {
                Employee employee = await employeeRepository.GetByEmpIdAsync(empId);
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
                await employeeRepository.InsertEmployeeAsync(employee);
                string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketTypeSvc/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client2.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketSvc/") };
                client3.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client3.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
                return Created($"api/Employee/{employee.EmpId}", employee);
            }
            catch(EmployeeException ex)
            {
                return BadRequest(new {Message = ex.Message});
            }
        }
        [HttpPut("{empId}")]
        public async Task<ActionResult> Edit(int empId,Employee employee)
        {
            try
            {
                await employeeRepository.UpdateEmployeeAsync(empId,employee);
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
              string userName = "Suresh";
                string role = "admin";
                string secretKey = "My name is Maximus Decimas Meridias, Husband to a murderd wife, Father to a murderd Son";
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/authSvc/") };
                string token = await client1.GetStringAsync($"{userName}/{role}/{secretKey}");
                HttpClient client2 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketTypeSvc/") };
                client2.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response1=await client2.DeleteAsync("FromEmployee/" + empId );
                HttpClient client3 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketSvc/") };
                client3.DefaultRequestHeaders.Authorization = new
                    System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response2=await client3.DeleteAsync("Employee/" + empId );
                if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode)
                {
                    await employeeRepository.DeleteEmployeeAsync(empId);
                    return Ok();
                }
                else
                {
                    if (response1.IsSuccessStatusCode)
                    {
                        HttpClient client4 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketTypeSvc/") };
                        client4.DefaultRequestHeaders.Authorization = new 
                                        System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client4.PostAsJsonAsync("Employee/", new {EmpId = empId});
                    }
                    if (response2.IsSuccessStatusCode)
                    {
                        HttpClient client5 = new HttpClient() { BaseAddress = new Uri("http://localhost:5026/ticketSvc/") };
                        client5.DefaultRequestHeaders.Authorization = new
                                        System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client5.PostAsJsonAsync("Employee/", new { EmpId = empId });
                    }
                    string errorMessage = string.Empty;
                    if (!response1.IsSuccessStatusCode)
                    {
                       var errContent = await response1.Content.ReadAsStringAsync();
                       var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                       errorMessage += errorObj.GetProperty("message").GetString() + "\n";
                    }
                    if (!response2.IsSuccessStatusCode)
                    {
                        var errContent = await response2.Content.ReadAsStringAsync();
                        var errorObj = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(errContent);
                        errorMessage += errorObj.GetProperty("message").GetString();

                    }
                    return BadRequest(errorMessage);
                }
           
        }
    }
}
