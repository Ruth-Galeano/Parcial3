using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
using Parcial2.Services;

namespace Parcial2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioServices usuarioService;
        private IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.usuarioService = new UsuarioServices(configuration.GetConnectionString("postgresDB"));
        }

        [HttpPost("IniciarSesion")]
        public ActionResult<string> Login(UsuarioModels model)
        {
            return this.usuarioService.iniciarSesion(model, _configuration);
        }
    }
}
