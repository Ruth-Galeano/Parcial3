using Dapper;
using Parcial2.Models;

namespace Parcial2.Repositories
{
    public class UsuarioRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        
        public UsuarioRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public int verificarUsuario(UsuarioModels model)
        {
            try
            {
                return connection.QueryFirst<int>($"SELECT count(1) FROM usuario WHERE nombre_usuario = @usuario and password = @password", model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
