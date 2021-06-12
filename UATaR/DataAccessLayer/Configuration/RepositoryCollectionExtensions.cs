using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer.Configuration
{
    public static class RepositoryCollectionExtensions
    {
        public static void RegisterDependenciesDal(this IServiceCollection services, IConfiguration configuration, string connectionName)
        {
            string connection = configuration.GetConnectionString(connectionName);
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IGenericRepository<TeacherDto, int>, GenericRepository<TeacherDto>>();
            services.AddScoped<IGenericRepository<SubjectDto, int>, GenericRepository<SubjectDto>>();
            services.AddScoped<IGenericRepository<LoadTypeDto, int>, GenericRepository<LoadTypeDto>>();
            services.AddScoped<IGenericRepository<LoadDto, int>, GenericRepository<LoadDto>>();
            services.AddScoped<IGenericRepository<ExecuteLoadDto, int>, GenericRepository<ExecuteLoadDto>>();
            services.AddScoped<IGenericRepository<GroupDto, int>, GenericRepository<GroupDto>>();
        }
    }
}