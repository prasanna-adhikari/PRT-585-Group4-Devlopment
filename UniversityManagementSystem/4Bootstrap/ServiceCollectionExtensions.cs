using _3BusinessLogicLayer.Interfaces;
using _3BusinessLogicLayer.Services;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _4Bootstrap
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            // Register StudentService
            services.AddScoped<IStudentService, StudentService>();

            // Register DepartmentService
            services.AddScoped<IDepartmentService, DepartmentService>();

            // Register CourseService
            services.AddScoped<ICourseService, CourseService>();

            return services;
        }
    }

}
