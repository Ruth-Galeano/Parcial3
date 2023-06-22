
using Parcial2.Models;
using Parcial2.Repositories;
namespace Parcial2.Services
{
    public class PersonaServices
    {
        
        private PersonaRepository personaRepository;

        public PersonaServices(string connectionString)
        {
            this.personaRepository = new PersonaRepository(connectionString);
        }

        public string insertarPersona(PersonaModels persona)
        {
            return validarDatosPersona(persona) ? personaRepository.insertarPersona (persona) : throw new Exception("Error en la validacion");
        }

        public string modificarPersona(PersonaModels persona, int id)
        {
            if (personaRepository.consultarPersona(id) != null)
                return validarDatosPersona(persona) ?
                    personaRepository.modificarPersona(persona, id) :
                    throw new Exception("Error en la validacion");
            else
                return "No se encontraron los datos de esta Persona";
        }

        public string eliminarPersona(int id)
        {
            return personaRepository.eliminarPersona(id);
        }

        public PersonaModels consultarPersona(int id)
        {
            return personaRepository.consultarPersona(id);
        }

        public IEnumerable<PersonaModels> listarPersona()
        {
            return personaRepository.listarPersona();
        }

        private bool validarDatosPersona(PersonaModels persona)
        {
            if (persona.Nombre.Trim().Length < 2)
            {
                return false;
            }

            if (persona.Apellido.Trim().Length < 2)
            {
                return false;
            }

            if (persona.TipoDocumento.Trim().Length < 1)
            {
                return false;
            }

            if (persona.Documento.Trim().Length < 1)
            {
                return false;
            }

            if (persona.Direccion.Trim().Length < 2)
            {
                return false;
            }

            if (persona.Telefono.Trim().Length < 9)
            {
                return false;
            }

            if (persona.Mail.Trim().Length < 5)
            {
                return false;
            }

            return true;
        }
    }
}
