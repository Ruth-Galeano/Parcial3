using Dapper;
using Parcial2.Models;

namespace Parcial2.Repositories
{
    public class HistorialRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public HistorialRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }
        public string insertarHistorial(HistorialModels historial)
        {
            try
            {
                connection.Execute("insert into historial (fecha, monto, operacion, id_cuenta) " +
                    " values(@fecha, @monto, @operacion, @idCuenta )", historial);
                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<HistorialModels> listarExtracto(int idCuenta)
        {
            try
            {
                return connection.Query<HistorialModels>($"SELECT id, fecha, monto, operacion, id_cuenta as IdCuenta FROM historial WHERE id_cuenta = {idCuenta} order by id asc");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
