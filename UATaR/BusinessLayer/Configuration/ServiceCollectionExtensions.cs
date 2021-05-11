using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependenciesBll(this IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            services.RegisterDependenciesDal(configuration, connectionName);

            services.AddScoped<IService<Teacher, int>, TeacherService>();
            services.AddScoped<IService<Subject, int>, SubjectService>();
            services.AddScoped<IService<Speciality, int>, SpecialityService>();
            services.AddScoped<IService<LoadType, int>, LoadTypeService>();
            services.AddScoped<IService<Load, int>, LoadService>();
            services.AddScoped<IService<ExecuteLoad, int>, ExecuteLoadService>();
        }
    }
}