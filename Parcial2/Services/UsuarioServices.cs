using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Parcial2.Models;
using Parcial2.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Parcial2.Services
{
    public class UsuarioServices
    {
        private UsuarioRepository usuarioRepository;
       
        public UsuarioServices(string connectionString)
        {
            this.usuarioRepository = new UsuarioRepository(connectionString);
        }

        public string iniciarSesion(UsuarioModels model, IConfiguration configuration)
        {
            int usuario = usuarioRepository.verificarUsuario(model);
            if(usuario > 0) {

                string secretKey = configuration.GetSection("Jwt:Key").Value;
                
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("usuario", model.Usuario)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    null,
                    null,
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: signIn
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            return "Usuario o contraseña inválido";
        }
    }
}
