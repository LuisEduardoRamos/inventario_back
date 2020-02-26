using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventarioApi.Models{
    public class Usuario{
        [Key]
        public int id {get; set;}
        public string Nombre {get; set;}
        public string Correo {get; set;}
        public string Telefono {get; set;}
        public string NombreUsuario {get; set;}
        public string Password {get; set;}
        [ForeignKey("Sucursal")]
        public int Sucursal {get; set;}
    }
}