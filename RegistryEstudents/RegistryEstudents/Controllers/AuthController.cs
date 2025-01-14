using Microsoft.AspNetCore.Mvc;
using RegistryEstudents.Models;
using RegistryEstudents.Models.DTO;
using RegistryEstudents.Services;
using RegistryEstudents.Utils;

namespace RegistryEstudents.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthServices _iauthServices;
        private readonly Utility _utility;

        public AuthController(IAuthServices iauthServices,Utility utility) { 
            this._iauthServices = iauthServices;
            _utility = utility;
        }

        [HttpPost("logonEstudents")]
        public async Task<IActionResult> LogonEstudents(LoginDTO loginDTO)
        {
                var response = await _iauthServices.LogonEstudents(loginDTO);
            if (response != null) {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = true,user = response ,token = _utility.generateJWT(response) });
                }
            else {
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, token = "" });
                    }
        }
    }
}
