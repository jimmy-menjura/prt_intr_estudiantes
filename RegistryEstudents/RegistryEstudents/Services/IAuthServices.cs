using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;

namespace RegistryEstudents.Services
{
    public interface IAuthServices
    {
        public Task<Estudents> LogonEstudents(LoginDTO loginDTO);
    }
}
