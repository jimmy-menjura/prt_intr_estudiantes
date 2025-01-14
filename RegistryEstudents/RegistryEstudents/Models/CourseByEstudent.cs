namespace RegistryEstudents.Models
{
    public class CourseByEstudent
    {
        public int Id { get; set; }
        public string Estudent { get; set; }
        public int idEstudents {get; set; }
        public string Teacher { get; set; }
        public int idTeacher { get; set; }
        public string Course { get; set; }
        public int idCourse {get; set; }
        public string NoCredits { get; set; }
        public int idNoCredits {get; set; }
    }
}
