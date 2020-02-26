using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventarioApi.Models{
    public class Producto{
        [Key]
        public int id {get; set;}
        public string Nombre {get; set;}
        public string Descripcion {get; set;}
        [ForeignKey("Proveedor")]
        public int Proveedor {get; set;} 
        [ForeignKey("CategoriaProducto")]
        public int CategoriaProducto {get; set;}
    }
}