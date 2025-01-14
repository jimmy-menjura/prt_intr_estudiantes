using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;
using System.Data;


namespace RegistryEstudents.Services
{
    public class EstudentsServices : IEstudentsServices
    {
        private readonly DBContextClass _dBContextClass;

        private readonly RegistryEstudents.Utils.Utility _utility;

        public EstudentsServices(DBContextClass dBContextClass, RegistryEstudents.Utils.Utility utility)
        {
            _dBContextClass = dBContextClass;
            _utility = utility;
        }
        public async Task<List<Estudents>> GetEstudents()
        {
            return await _dBContextClass.Estudents
                .FromSqlRaw<Estudents>("sp_getEstudents")
                .ToListAsync();
        }

        public async Task<IEnumerable<Estudents>> GetEstudentsById(int id)
        {
            var param = new SqlParameter("@id", id);
            var dataEstudents = await Task.Run(() => _dBContextClass.Estudents
                .FromSqlRaw("exec sp_getEstudentById @id", param).ToListAsync());
            return dataEstudents;
        }

        public async Task<int> SaveEstudents(Estudents estudents)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@name", estudents.name));
            param.Add(new SqlParameter("@phone", estudents.phone));
            param.Add(new SqlParameter("@mail", estudents.mail));
            param.Add(new SqlParameter("@password", _utility.encryptSHA256(estudents.password)));

            var response = await Task.Run(() => _dBContextClass.Database
            .ExecuteSqlRawAsync(@"exec sp_saveEstudents @name,@phone,@mail,@password ", param.ToArray()));
            return response;

        }

        public async Task<int> UpdateEstudents(Estudents estudents)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@id", estudents.id));
            param.Add(new SqlParameter("@name", estudents.name));
            param.Add(new SqlParameter("@phone", estudents.phone));
            param.Add(new SqlParameter("@mail", estudents.mail));
            param.Add(new SqlParameter("@password", estudents.password));

            var response = await Task.Run(() => _dBContextClass.Database
            .ExecuteSqlRawAsync(@"exec sp_updateEstudents @id,@name,@phone,@mail,@password ", param.ToArray()));
            return response;

        }
        public async Task<int> DeleteEstudents(int id)
        {
            return await Task.Run(() => _dBContextClass.Database.ExecuteSqlInterpolatedAsync($"sp_deleteEstudentById {id}"));
        }

        //COURSES
        public async Task<List<CourseByEstudent>> GetAllcourse()
        {
            return await _dBContextClass.CourseByEstudents
                .FromSqlRaw<CourseByEstudent>("sp_getAllCourse")
                .ToListAsync();
        }

        public async Task<List<CourseByEstudent>> GetcourseByEstudent(string id)
        {
            var param = new SqlParameter("@id", Int32.Parse(id));
            return await _dBContextClass.CourseByEstudents
                .FromSqlRaw<CourseByEstudent>("sp_getcourseByEstudent @id", param)
                .ToListAsync();
        }
        public async Task<List<CourseByTeacher>> GetcourseByTeacher()
        {
            return await _dBContextClass.GetcourseByTeacher
                .FromSqlRaw<CourseByTeacher>("sp_getcourseByteacher")
                .ToListAsync();
        }
        public async Task<List<string>> SaveCourseByEstudents(List<CourseByTeacherDTO> courseByTeachersEstudent)
        {
            
            var resultMessages = new List<string>();
            foreach (var item in courseByTeachersEstudent)
            {
                var resultMessage = new SqlParameter
                {
                    ParameterName = "@message",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 500,
                    Direction = System.Data.ParameterDirection.Output
                };
                var id = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                var param1 = new SqlParameter("@id_estudents", item.idEstudents);
                var param2 = new SqlParameter("@id_teacher", item.idTeacher);
                var param3 = new SqlParameter("@id_credits", item.idNoCredits);
                var param4 = new SqlParameter("@id_course", item.idCourse);

                var response = await Task.Run(() => _dBContextClass.Database
                .ExecuteSqlRaw("sp_saveAndValidatecourseByEstudent @id_estudents,@id_teacher,@id_credits,@id_course,@message output,@id output",
                    param1,param2,param3,param4, resultMessage,id
                ));
                var idValue = (int)(id.Value ?? 0);
                var resultadoMensaje = resultMessage.Value.ToString();

                resultMessages.Add($"{resultadoMensaje}");
            }
            return resultMessages;

        }
        public async Task<int> GetcourseRegisterByEstudent(int id)
        {
            var ident = new SqlParameter("@id", id);
            var returnNumberRegister = new SqlParameter
            {
                ParameterName = "@idReturn",
                SqlDbType = System.Data.SqlDbType.Int,
                Size = 500,
                Direction = System.Data.ParameterDirection.Output
            };
            var response = await Task.Run(() => _dBContextClass.Database
            .ExecuteSqlRaw("sp_getcourseRegisterByEstudent @id, @idReturn output", ident, returnNumberRegister));
            var countDb = (int)(returnNumberRegister.Value ?? 0);
            return countDb;
        }
        public async Task<List<CourseByEstudent>> GetcourseById(string id)
        {
            var param = new SqlParameter("@id", Int32.Parse(id));
            return await _dBContextClass.CourseByEstudents
                .FromSqlRaw<CourseByEstudent>("sp_getcourseById @id", param)
                .ToListAsync();
        }
        public async Task<int> DeleteCourseById(int idEstudent,int idCourse)
        {
            return await Task.Run(() => _dBContextClass.Database.ExecuteSqlInterpolatedAsync($"sp_deletecourseById {idEstudent},{idCourse}"));
        }
    }
    }
