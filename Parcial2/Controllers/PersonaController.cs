using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
using Parcial2.Services;

namespace Parcial2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PersonaController : Controller
    {
        private PersonaServices personaService;
        private IConfiguration _configuration;

        public PersonaController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.personaService = new PersonaServices(configuration.GetConnectionString("postgresDB"));
        }
        [HttpGet("ListarPersona")]
        public ActionResult<List<PersonaModels>> ListarPersona()
        {
            var resultado = personaService.listarPersona();
            return Ok(resultado);
        }
        [HttpGet("ConsultarPersona/{id}")]
        public ActionResult<PersonaModels> ConsultarPersona(int id)
        {
            var resultado = this.personaService.consultarPersona(id);
            return Ok(resultado);
        }
        [HttpPost("InsertarPersona")]
        public ActionResult<string> insertarPersona(PersonaModels modelo)
        {
            var resultado = this.personaService.insertarPersona(new PersonaModels
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Mail = modelo.Mail,
                Telefono = modelo.Telefono,
                Documento = modelo.Documento,
                TipoDocumento = modelo.TipoDocumento,
                Direccion = modelo.Direccion,
                Estado = modelo.Estado,
            });
            return Ok(resultado);
        }
        [HttpPut("modificarPersona/{id}")]
        public ActionResult<string> modificarPersona(PersonaModels modelo, int id)
        {
            var resultado = this.personaService.modificarPersona(new PersonaModels
            {
                Nombre = modelo.Nombre,
                Apellido = modelo.Apellido,
                Mail = modelo.Mail,
                Telefono = modelo.Telefono,
                Documento = modelo.Documento,
                TipoDocumento = modelo.TipoDocumento,
                Direccion = modelo.Direccion,
                Estado = modelo.Estado,
            }, id);
            return Ok(resultado);
        }
        [HttpDelete("eliminarPersona/{id}")]
        public ActionResult<string> eliminarPersona(int id)
        {
            var resultado = this.personaService.eliminarPersona(id);
            return Ok(resultado);
        }
    }
}

