using AutoMapper;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Dtos;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Getemployees()
        {
            var employee = await _context.Employees.ToListAsync();
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employee);
            return Ok(employeeDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> Getemployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Addemployee([FromBody] CreateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            var employeedto = _mapper.Map<EmployeeDto>(employee);
            return CreatedAtAction("GetEmployee",new {id = employeedto.Id}, employeedto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Updateemployee(int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }
            _mapper.Map(updateEmployeeDto, employee);
            _context.Entry(employee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteemployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Filteremployee(int salary)
        {
            var employees = await _context.Employees.Where(e => e.Salary > salary).ToListAsync();
            var employeeDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDto);
        }
    }
}
