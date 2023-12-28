using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Serialization;

namespace Demo.PL.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IUnitOfWork,UnitOfWork> ();
            return services;
        }
    }
}
