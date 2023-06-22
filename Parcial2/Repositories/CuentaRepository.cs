using Dapper;
using Parcial2.Enums;
using Parcial2.Models;

namespace Parcial2.Repositories
{
    public class CuentaRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public CuentaRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }
        public string insertarCuenta(CuentaModels cuenta)
        {
            try
            {
                connection.Execute("insert into CUENTA (id_cuenta, id_persona, nombre_cuenta,numero_cuenta, saldo, limite_saldo, limite_transferencia, estado) " +
                    " values(@idCuenta, @idPersona, @nombreCuenta, @numeroCuenta, @saldo, @limiteSaldo, @limiteTransferencia, @estado )", cuenta);
                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string modificarCuenta(CuentaModels cuenta, int id)
        {
            try
            {
                connection.Execute($"UPDATE cuenta SET " +
                    "id_cuenta = @idCuenta, " +
                    "id_persona = @idPersona, " +
                    "nombre_cuenta = @nombreCuenta," +
                    "numero_cuenta = @numeroCuenta,"+
                    "saldo = @saldo,"+
                    "limite_saldo = @limiteSaldo," +
                    "limite_transferencia = @limiteTransferencia,"+
                    "estado = @estado "+
                    $"WHERE id = {id}", cuenta);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string eliminarCuenta(int id, EstadoCuenta estado)
        {
            try
            {
                connection.Execute("UPDATE cuenta SET estado = @Estado WHERE id = @Id", new { Estado = estado, Id = id });
                return "Se eliminó correctamente el registro...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CuentaModels consultarCuenta(int id)
        {
            try
            {
                return connection.QueryFirst<CuentaModels>($"SELECT id, id_cuenta as IdCuenta, nombre_cuenta as NombreCuenta, numero_cuenta as NumeroCuenta, saldo, limite_saldo as LimiteSaldo, limite_transferencia as LimiteTransferencia, estado, id_persona as IdPersona FROM cuenta WHERE id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CuentaModels> listarCuenta()
        {
            try
            {
                return connection.Query<CuentaModels>($"SELECT id, id_cuenta as IdCuenta, nombre_cuenta as NombreCuenta, numero_cuenta as NumeroCuenta, saldo, limite_saldo as LimiteSaldo, limite_transferencia as LimiteTransferencia, estado, id_persona as IdPersona  FROM cuenta order by id asc");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CuentaModels buscarCuenta(string numeroCuenta)
        {
            try
            {
                return connection.QueryFirst<CuentaModels>($"SELECT id, id_cuenta as IdCuenta, nombre_cuenta as NombreCuenta, numero_cuenta as NumeroCuenta, saldo, limite_saldo as LimiteSaldo, limite_transferencia as LimiteTransferencia, estado, id_persona as IdPersona FROM cuenta WHERE numero_cuenta = @NumeroCuenta",new { NumeroCuenta = numeroCuenta });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
