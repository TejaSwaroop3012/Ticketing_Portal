using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeLibrary.Models;
using EmployeeLibrary.Repos;
using Microsoft.AspNetCore.Authorization;


namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
                HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5031/api/TicketType/") };
                await client.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
                HttpClient client1 = new HttpClient() { BaseAddress = new Uri("http://localhost:5185/api/Ticket/") };
                await client1.PostAsJsonAsync("Employee", new { EmpId = employee.EmpId });
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
                await employeeRepository.DeleteEmployee(empId);
                return Ok();
            }
            catch(EmployeeException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
