using System.ComponentModel.DataAnnotations;
namespace Parcial2.Models
{
    public class PersonaModels
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoDocumento { get; set;}
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }
        public Boolean? Estado { get; set; }
    }
}

