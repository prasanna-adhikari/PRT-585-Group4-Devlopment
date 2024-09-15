using _1CommonInfrastructure.Models;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication3tierApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return Ok(await _departmentService.GetAllDepartmentsAsync());
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound("Department not found");
            }

            return Ok(department);
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.DepartmentId }, createdDepartment);
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest("Department ID mismatch");
            }

            var success = await _departmentService.UpdateDepartmentAsync(department);
            if (!success)
            {
                return NotFound("Department not found");
            }

            return NoContent();
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var success = await _departmentService.DeleteDepartmentAsync(id);
            if (!success)
            {
                return NotFound("Department not found");
            }

            return NoContent();
        }

        // PATCH: api/Departments/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchDepartment(int id, [FromBody] JsonPatchDocument<Department> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document");
            }

            var success = await _departmentService.PatchDepartmentAsync(id, patchDoc);
            if (!success)
            {
                return NotFound("Department not found");
            }

            return Ok(new { message = "Department updated successfully" });
        }
    }
}
