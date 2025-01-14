using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;

namespace RegistryEstudents.Services
{
    public interface IEstudentsServices
    {
        public Task<List<Estudents>> GetEstudents();
        public Task<IEnumerable<Estudents>> GetEstudentsById(int id);

        public Task<int> SaveEstudents(Estudents estudents);

        public Task<int> UpdateEstudents(Estudents estudents);
        public Task<int> DeleteEstudents(int id);
        public Task<List<CourseByEstudent>> GetcourseByEstudent(string id);
        public Task<List<String>> SaveCourseByEstudents(List<CourseByTeacherDTO> courseByTeachersEstudent);
        public Task<List<CourseByEstudent>> GetAllcourse();
        public Task<List<CourseByTeacher>> GetcourseByTeacher();

        public Task<int> GetcourseRegisterByEstudent(int id);
        public Task<List<CourseByEstudent>> GetcourseById(string id);

        public Task<int> DeleteCourseById(int idEstudent, int idCourse);

    }
}
