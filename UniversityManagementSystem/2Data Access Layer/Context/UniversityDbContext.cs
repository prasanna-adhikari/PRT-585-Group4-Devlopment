using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _1CommonInfrastructure.Models;

namespace _2DataAccessLayer.Context
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
        }

        // Define the DbSet for the Student model
        public DbSet<Student> Students { get; set; }

        // Add DbSets for other models below
    }
}
