using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Getemployees()
        {
            return Ok(_context.Employees);
        }
        [HttpGet("{id}")]
        public IActionResult Getemployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]
        public IActionResult Addemployee([FromBody] Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public IActionResult Updateemployee(int id, [FromBody] Employee employee)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            emp.Name = employee.Name;
            emp.Salary = employee.Salary;
            emp.JoiningDate = employee.JoiningDate;
            _context.SaveChanges();
            return Ok(emp);
        }

        [HttpDelete("{id}")]
        public IActionResult Deleteemployee(int id)
        {
            var emp = _context.Employees.Find(id);
            if (emp == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(emp);
            _context.SaveChanges();
            return Ok(emp);
        }

        [HttpGet("Filter")]
        public IActionResult Filteremployee(int salary)
        {
            var employees = _context.Employees.Where(e => e.Salary > salary);
            return Ok(employees);
        }
    }
}
