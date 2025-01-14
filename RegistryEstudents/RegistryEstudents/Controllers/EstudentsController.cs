using Microsoft.AspNetCore.Mvc;
using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;
using RegistryEstudents.Services;

namespace RegistryEstudents.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudentsController : Controller
    {
        private readonly IEstudentsServices _estudentsServices;

        public EstudentsController(IEstudentsServices estudentsServices) { 
            this._estudentsServices = estudentsServices;
        }
        [HttpGet("getEstudents")]
        public async Task<List<Estudents>> GetEstudents() {
            try { 
                 return await _estudentsServices.GetEstudents();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPost("saveEstudents")]
        public async Task<IActionResult> SaveEstudents(Estudents estudents)
        {
            if (estudents == null) {
                return BadRequest(estudents);
            }
            try
            {
                var response =  await _estudentsServices.SaveEstudents(estudents);
                
                return Ok(response);

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpPut("updateEstudents")]
        public async Task<IActionResult> UpdateEstudents(Estudents estudents)
        {
            if (estudents == null)
            {
                return BadRequest(estudents);
            }
            try
            {
                var response = await _estudentsServices.UpdateEstudents(estudents);

                return Ok(response);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("deleteEstudents")]
        public async Task<int> DeleteEstudents(int id)
        {
            
            try
            {
                var response = await _estudentsServices.DeleteEstudents(id);

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //COURSES

        [HttpGet("getcourseByEstudent/{id}")]
        public async Task<List<CourseByEstudent>> getcourseByEstudent(string id)
        {
            try
            {
                return await _estudentsServices.GetcourseByEstudent(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("getcourseByTeacher")]
        public async Task<List<CourseByTeacher>> getcourseByTeacher()
        {
            try
            {
                return await _estudentsServices.GetcourseByTeacher();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("getAllCourse")]
        public async Task<List<CourseByEstudent>> GetAllCourse()
        {
            try
            {
                return await _estudentsServices.GetAllcourse();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost("saveCourseByEstudents")]
        public async Task<IActionResult> saveCourseByEstudents(List<CourseByTeacherDTO> courseByTeachersEstudent)
        {
            if (courseByTeachersEstudent == null)
            {
                return BadRequest(courseByTeachersEstudent);
            }
            try
            {
                var response = await _estudentsServices.SaveCourseByEstudents(courseByTeachersEstudent);

                return Ok(response);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("getcourseRegisterByEstudent/{id}")]
        public async Task<int> GetcourseRegisterByEstudent(int id)
        {

            try
            {
                var response = await _estudentsServices.GetcourseRegisterByEstudent(id);

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("getcourseById/{id}")]
        public async Task<List<CourseByEstudent>> getcourseById(string id)
        {
            try
            {
                return await _estudentsServices.GetcourseById(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpDelete("deleteCourseById/{idEstudent}/{idCourse}")]
        public async Task<int> DeleteCourseById(int idEstudent, int idCourse)
        {

            try
            {
                var response = await _estudentsServices.DeleteCourseById(idEstudent, idCourse);

                return response;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
