using Dapper;
using Parcial2.Models;
namespace Parcial2.Repositories
{
    public class PersonaRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public PersonaRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }
        public string insertarPersona(PersonaModels persona)
        {
            try
            {
                connection.Execute("insert into persona(nombre, apellido, mail,tipo_documento, documento, telefono, estado, direccion) " +
                    " values(@nombre, @apellido, @mail, @tipoDocumento, @documento, @telefono,@estado, @direccion)", persona);
                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string modificarPersona(PersonaModels persona, int id)
        {
            try
            {
                connection.Execute($"UPDATE persona SET " +
                    "nombre = @nombre, " +
                    "apellido = @apellido, " +
                    "tipo_documento = @tipoDocumento, " +
                    "documento = @documento, " +
                    "telefono = @telefono, " +
                    "mail = @mail, " +
                    "direccion = @direccion, " +
                    "estado = @estado "+
                    $"WHERE id = {id}", persona);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string eliminarPersona(int id)
        {
            try
            {
                connection.Execute($"update persona set estado = false where id = {id}");
                return "Se eliminó correctamente el registro...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PersonaModels consultarPersona(int id)
        {
            try
            {
                return connection.QueryFirst<PersonaModels>($"SELECT id, nombre, apellido, tipo_documento as tipoDocumento, documento, direccion, telefono, mail, estado FROM  persona WHERE id = {id} and estado = true");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PersonaModels> listarPersona()
        {
            try
            {
                return connection.Query<PersonaModels>($"SELECT  id, nombre, apellido, tipo_documento as tipoDocumento, documento, direccion, telefono, mail, estado FROM persona where estado = true order by id asc");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

