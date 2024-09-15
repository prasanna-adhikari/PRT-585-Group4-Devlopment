using _1CommonInfrastructure.Models;
using _2DataAccessLayer.Context;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly UniversityDbContext _context;

        public DepartmentService(UniversityDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<bool> UpdateDepartmentAsync(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PatchDepartmentAsync(int id, JsonPatchDocument<Department> patchDoc)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            patchDoc.ApplyTo(department);  // Apply the patch changes
            await _context.SaveChangesAsync();  // Save the changes to the database

            return true;
        }
    }
}
