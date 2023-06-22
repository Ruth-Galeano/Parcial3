using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
using Parcial2.Services;

namespace Parcial2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CuentaController : Controller
    {

        private CuentaServices cuentaService;
        private IConfiguration _configuration;
        public CuentaController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.cuentaService = new CuentaServices(configuration.GetConnectionString("postgresDB"));
        }
        [HttpGet("ListarCuenta")]
        public ActionResult<List<CuentaModels>> ListarCuenta()
        {
            var resultado = cuentaService.listarCuenta();
            return Ok(resultado);
        }
        [HttpGet("ConsultarCuenta/{id}")]
        public ActionResult<CuentaModels> ConsultarCuenta(int id)
        {
            var resultado = this.cuentaService.consultarCuenta(id);
            return Ok(resultado);
        }

        [HttpPost("InsertarCuenta")]
        public ActionResult<string> insertarCuenta(CuentaModels modelo)
        {
            var resultado = this.cuentaService.insertarCuenta(modelo);
            return Ok(resultado);
        }

        [HttpPut("modificarCuenta/{id}")]
        public ActionResult<string> modificarCiudad(CuentaModels modelo, int id)
        {
            var resultado = this.cuentaService.modificarCuenta(modelo, id);
            return Ok(resultado);
        }

        [HttpDelete("eliminarCuenta/{id}")]
        public ActionResult<string> eliminarCuenta(int id)
        {
            var resultado = this.cuentaService.eliminarCuenta(id);
            return Ok(resultado);
        }
    }
}
    

