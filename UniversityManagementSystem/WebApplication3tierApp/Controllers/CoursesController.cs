using _1CommonInfrastructure.Models;
using _3BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication3tierApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _courseService.GetAllCoursesAsync());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound("Course not found");
            }

            return Ok(course);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            var createdCourse = await _courseService.CreateCourseAsync(course);
            return CreatedAtAction(nameof(GetCourse), new { id = createdCourse.CourseId }, createdCourse);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest("Course ID mismatch");
            }

            var success = await _courseService.UpdateCourseAsync(course);
            if (!success)
            {
                return NotFound("Course not found");
            }

            return NoContent();
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var success = await _courseService.DeleteCourseAsync(id);
            if (!success)
            {
                return NotFound("Course not found");
            }

            return NoContent();
        }

        // PATCH: api/Courses/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, [FromBody] JsonPatchDocument<Course> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document");
            }

            var success = await _courseService.PatchCourseAsync(id, patchDoc);
            if (!success)
            {
                return NotFound("Course not found");
            }

            return Ok(new { message = "Course updated successfully" });
        }
    }
}
