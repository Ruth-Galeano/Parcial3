using Parcial2.Enums;

namespace Parcial2.Models
{
    public class CuentaModels
    {
        
        public int Id { get; set; }
        public int IdCuenta { get; set; }
        public string NombreCuenta { get; set; }    
        public string NumeroCuenta { get; set; }
        public int Saldo { get; set; }
        public int LimiteSaldo { get; set; }
        public int LimiteTransferencia { get; set; }
        public EstadoCuenta Estado { get; set; }
        public int IdPersona { get; set; } 
        public PersonaModels? Persona { get; set; }
    }
}

