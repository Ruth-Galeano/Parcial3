namespace Parcial2.Models
{
    public class HistorialModels
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Monto { get; set; }
        public string Operacion { get; set; }
        public int IdCuenta { get; set; }
    }
}
