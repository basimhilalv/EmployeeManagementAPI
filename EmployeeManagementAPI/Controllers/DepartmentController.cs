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
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public DepartmentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments.ToListAsync();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
            return Ok(departmentDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return Ok(departmentDto);
        }
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDepartment", new { id = department.Id }, _mapper.Map<DepartmentDto>(department));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto departmentdto)
        {
            if (id != departmentdto.Id)
            {
                return BadRequest();
            }
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _mapper.Map(departmentdto, department);
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
