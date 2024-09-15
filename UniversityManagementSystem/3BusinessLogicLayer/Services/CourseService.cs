using _1CommonInfrastructure.Models;
using _2DataAccessLayer.Context;
using _3BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3BusinessLogicLayer.Services
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDbContext _context;

        public CourseService(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _context.Courses.Include(c => c.Department).ToListAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.Include(c => c.Department).FirstOrDefaultAsync(c => c.CourseId == id);
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchCourseAsync(int id, JsonPatchDocument<Course> patchDoc)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            patchDoc.ApplyTo(course);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
