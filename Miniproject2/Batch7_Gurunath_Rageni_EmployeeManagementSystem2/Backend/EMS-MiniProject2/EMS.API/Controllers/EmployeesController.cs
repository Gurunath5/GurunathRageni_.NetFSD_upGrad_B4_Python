using EMS.API.DTOs;
using EMS.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;

        public EmployeesController(EmployeeService service) => _service = service;

        /// <summary>Paginated employee list with server-side search, filter, sort.</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeQueryParams q)
        {
            var result = await _service.GetEmployeesAsync(q);
            return Ok(result);
        }

        /// <summary>Dashboard KPIs, department breakdown, and recent employees.</summary>
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _service.GetDashboardAsync();
            return Ok(result);
        }

        /// <summary>Get a single employee by ID.</summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _service.GetByIdAsync(id);
            return emp == null ? NotFound(new { message = "Employee not found." }) : Ok(emp);
        }

        /// <summary>Create a new employee. Admin only.</summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Email uniqueness check
            if (await _service.EmailExistsAsync(dto.Email))
                return Conflict(new { message = "An employee with this email already exists." });

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>Update an existing employee. Admin only.</summary>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Email uniqueness — exclude current employee
            if (await _service.EmailExistsAsync(dto.Email, excludeId: id))
                return Conflict(new { message = "Another employee already uses this email." });

            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound(new { message = "Employee not found." }) : Ok(updated);
        }

        /// <summary>Delete an employee. Admin only.</summary>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? Ok(new { message = "Employee deleted." }) : NotFound(new { message = "Employee not found." });
        }
    }
}