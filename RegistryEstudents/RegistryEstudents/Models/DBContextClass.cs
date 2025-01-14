using Microsoft.EntityFrameworkCore;
using RegistryEstudents.Models.DTO;

namespace RegistryEstudents.Models
{
    public class DBContextClass:DbContext
    {
        protected readonly IConfiguration Configuration;

        public DBContextClass(IConfiguration configuration) {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Estudents> Estudents { get; set; }

        public DbSet<CourseByEstudent> CourseByEstudents { get; set; }
        public DbSet<CourseByTeacher> GetcourseByTeacher { get; set; }

        public DbSet<MessageDTO> GetMessages { get; set; }




    }
}
