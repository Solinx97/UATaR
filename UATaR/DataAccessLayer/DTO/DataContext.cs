using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DTO
{
    public class DataContext : IdentityDbContext<UserDto>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<TeacherDto> Teacher { get; set; }

        public virtual DbSet<SubjectDto> Subject { get; set; }

        public virtual DbSet<SpecialityDto> Speciality { get; set; }

        public virtual DbSet<LoadTypeDto> LoadType { get; set; }

        public virtual DbSet<LoadDto> Load { get; set; }

        public virtual DbSet<ExecuteLoadDto> ExecuteLoad { get; set; }

        public virtual DbSet<GroupDto> Group { get; set; }
    }
}