using System.ComponentModel.DataAnnotations;
namespace webapi2.Models
{
    public class clientes
    {
        [Key]
        public int id_nombre { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }   
        public string direccion { get; set; }   
        public int id_departamento { get; set; }

    }
}
