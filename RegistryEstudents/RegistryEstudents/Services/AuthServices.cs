using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;

namespace RegistryEstudents.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly DBContextClass _dBContextClass;
        private readonly RegistryEstudents.Utils.Utility _utility;

        public AuthServices(DBContextClass dBContextClass, RegistryEstudents.Utils.Utility utility) { 
            _dBContextClass = dBContextClass;
            _utility = utility;
        }
        public async Task<Estudents> LogonEstudents(LoginDTO loginDTO)
        {
            var param = new List<SqlParameter>();
            var newObj = new Estudents();
            param.Add(new SqlParameter("@mail", loginDTO.mail));
            param.Add(new SqlParameter("@password", _utility.encryptSHA256(loginDTO.password)));

            var response = await Task.Run(() => _dBContextClass.Estudents
            .FromSqlRaw<Estudents>("exec sp_logonEstudent @mail,@password ", param.ToArray()).ToListAsync());

            response.ForEach(x =>
            {
                newObj.id = x.id;
                newObj.name = x.name;
                newObj.password = x.password;
                newObj.phone = x.phone;
                newObj.mail = x.mail;
            });
            return newObj;

        }

    }
}
