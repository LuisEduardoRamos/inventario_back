using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models{
    public class CategoriaProducto{
        [Key]
        public int id {get; set;}
        public string Nombre {get; set;}
        public string Restricciones {get; set;}
    }
}