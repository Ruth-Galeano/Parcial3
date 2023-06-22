

using Parcial2.Enums;
using Parcial2.Models;
using Parcial2.Repositories;

namespace Parcial2.Services
{
    public class OperacionServices
    {
        private CuentaRepository cuentaRepository;
        private HistorialRepository historialRepository;

        public OperacionServices(string connectionString)
        {
            this.cuentaRepository    = new CuentaRepository(connectionString);
            this.historialRepository = new HistorialRepository(connectionString);
        }

        public string Depositar(OperacionesRequestModels model)
        {
            CuentaModels cuenta = cuentaRepository.buscarCuenta(model.Cuenta);
            if (cuenta.Estado == EstadoCuenta.ACTIVO)
            {
                if (verificarLimite(cuenta, model.Monto))
                {
                    cuenta.Saldo = cuenta.Saldo + model.Monto;
                    cuentaRepository.modificarCuenta(cuenta, cuenta.Id);
                    agregarHistorial(new HistorialModels{
                        Fecha = DateTime.Now,
                        Monto = model.Monto,
                        Operacion = "Deposito",
                        IdCuenta = cuenta.Id
                    });
                    return "Depósito exitoso!";
                }
                return "Saldo de la cuenta supera el límite permitido";
            }
            return "No se pudo realizar el depósito!";
        }

        public string Retirar(OperacionesRequestModels model)
        {
            CuentaModels cuenta = cuentaRepository.buscarCuenta(model.Cuenta);
            if (cuenta.Estado == EstadoCuenta.ACTIVO)
            {
                if (verificarSaldo(cuenta, model.Monto))
                {
                    cuenta.Saldo = cuenta.Saldo - model.Monto;
                    cuentaRepository.modificarCuenta(cuenta, cuenta.Id);
                    agregarHistorial(new HistorialModels
                    {
                        Fecha = DateTime.Now,
                        Monto = model.Monto,
                        Operacion = "Retiro",
                        IdCuenta = cuenta.Id
                    });
                    return "Retiro exitoso!";
                }
                return "Saldo insuficiente";
            }
            return "No se pudo realizar el retiro!";
        }

        public string BloquearCuenta(string numeroCuenta)
        {
            CuentaModels cuenta = cuentaRepository.buscarCuenta(numeroCuenta);
            cuentaRepository.eliminarCuenta(cuenta.Id, EstadoCuenta.BLOQUEADO);
            return "Cuenta bloqueada";
        }

        public string Transferir(TransferenciaRequestModels model)
        {
            if(model.CuentaReceptor.Equals(model.CuentaRemitente))
            {
                return "La transferencia no puede realizarse a la misma cuenta";
            }

            CuentaModels cuentaRemitente = cuentaRepository.buscarCuenta(model.CuentaRemitente);
            CuentaModels cuentaReceptora = cuentaRepository.buscarCuenta(model.CuentaReceptor);

            if (cuentaRemitente.Estado != EstadoCuenta.ACTIVO)
            {
                return "La cuenta del remitente no está activo";
            }
            if (cuentaReceptora.Estado != EstadoCuenta.ACTIVO)
            {
                return "La cuenta del receptora no está activo";
            }

            if (!verificarSaldo(cuentaRemitente, model.Monto))
            {
                return "La transferencia no puede realizarse por insuficiencia de saldo";
            }
            if(!verificarLimite(cuentaReceptora, model.Monto))
            {
                return "La transferencia no puede realizarse por limite de saldo";
            }

            int cantidadTransferencia = (obtenerHistorial(cuentaRemitente.Id)).Count();
            if(cantidadTransferencia > cuentaRemitente.LimiteTransferencia)
            {
                return "Se ha llegado al límite de trasferencia";
            }


            cuentaRemitente.Saldo = cuentaRemitente.Saldo - model.Monto;
            cuentaRepository.modificarCuenta(cuentaRemitente, cuentaRemitente.Id);
            agregarHistorial(new HistorialModels
            {
                Fecha = DateTime.Now,
                Monto = model.Monto,
                Operacion = "Transferencia",
                IdCuenta = cuentaRemitente.Id
            });

            cuentaReceptora.Saldo = cuentaReceptora.Saldo + model.Monto;
            cuentaRepository.modificarCuenta(cuentaReceptora, cuentaReceptora.Id);
            agregarHistorial(new HistorialModels
            {
                Fecha = DateTime.Now,
                Monto = model.Monto,
                Operacion = "Recibido",
                IdCuenta = cuentaReceptora.Id
            });

            return "Transferencia realizada";
        }

        private bool verificarLimite(CuentaModels cuenta, int monto)
        {
            int saldo = cuenta.Saldo + monto;
            return cuenta.LimiteSaldo >= saldo ? true : false;
        }

        private bool verificarSaldo(CuentaModels cuenta, int monto)
        {
            int saldo = cuenta.Saldo - monto;
            return (saldo >= 0) ? true : false;
        }

        private void agregarHistorial (HistorialModels model)
        {
            historialRepository.insertarHistorial(model);
        }

        public IEnumerable<HistorialModels> ImprimirExtractoPorNumCuenta(string numeroCuenta)
        {
            CuentaModels cuenta = cuentaRepository.buscarCuenta(numeroCuenta);
            return obtenerHistorial(cuenta.Id);
        }
        private IEnumerable<HistorialModels> obtenerHistorial(int idCuenta)
        {
            return historialRepository.listarExtracto(idCuenta);
        }
    }
}