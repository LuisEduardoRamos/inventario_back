using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models{
    public class Sucursal{
        [Key]
        public int id {get; set;}
        public string Nombre {get; set;}
        public string Direccion {get; set;}
        public string Contacto {get; set;}
        public string Latitud {get; set;}
        public string Longitud {get; set;}
    }
}