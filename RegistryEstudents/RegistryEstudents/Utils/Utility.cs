using Microsoft.IdentityModel.Tokens;
using RegistryEstudents.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RegistryEstudents.Utils
{
    public class Utility
    {
        private readonly IConfiguration _configuration;
        public Utility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encryptSHA256(string texto)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computar el hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

                // Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string generateJWT(Estudents estudents)
        {
            //crear la informacion del usuario para token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, estudents.id.ToString()),
                new Claim(ClaimTypes.Email, estudents.mail!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
