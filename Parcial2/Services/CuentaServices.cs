using Parcial2.Enums;
using Parcial2.Models;
using Parcial2.Repositories;

namespace Parcial2.Services
{
    public class CuentaServices
    {
        private CuentaRepository repositoryCuenta;
        private PersonaRepository personaRepository;

        public CuentaServices(string connectionString)
        {
            this.repositoryCuenta = new CuentaRepository(connectionString);
            this.personaRepository = new PersonaRepository(connectionString);
        }

        public string insertarCuenta(CuentaModels cuenta)
        {
            return validarDatosCuenta(cuenta) ? repositoryCuenta.insertarCuenta(cuenta) : throw new Exception("Error en la validacion");
        }

        public string modificarCuenta(CuentaModels cuenta, int id)
        {
            if (repositoryCuenta.consultarCuenta(id) != null)
                return validarDatosCuenta(cuenta) ?
                    repositoryCuenta.modificarCuenta(cuenta, id) :
                    throw new Exception("Error en la validacion");
            else
                return "No se encontraron los datos de esta persona";
        }

        public string eliminarCuenta(int id)
        {
            return repositoryCuenta.eliminarCuenta(id, EstadoCuenta.INACTIVO);
        }

        public CuentaModels consultarCuenta(int id)
        {
            CuentaModels cuenta = repositoryCuenta.consultarCuenta(id);
            cuenta.Persona      = personaRepository.consultarPersona(cuenta.IdPersona);
            return cuenta;
        }

        public IEnumerable<CuentaModels> listarCuenta()
        {
            IEnumerable<CuentaModels> lista = repositoryCuenta.listarCuenta();
            foreach (var cuenta in lista)
            {
                cuenta.Persona = personaRepository.consultarPersona(cuenta.IdPersona);
            }
            return lista;
        }

        private bool validarDatosCuenta(CuentaModels cuenta)
        {
            if (cuenta.NumeroCuenta.Trim().Length < 2)
            {
                return false;
            }

            if (cuenta.NombreCuenta.Trim().Length < 2)
            {
                return false;
            }

            if (cuenta.Saldo < 0)
            {
                return false;
            }

            if (cuenta.LimiteSaldo < 0)
            {
                return false;
            }

            if (cuenta.LimiteTransferencia < 0)
            {
                return false;
            }

            return true;
        }
    }
}

