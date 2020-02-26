using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models{
    public class Proveedor{
        [Key]
        public int id {get; set;}
        public string Nombre {get; set;}
        public string Contacto {get; set;}
        public string Telefono {get; set;}

        public string Direccion { get; set;}
    }
}