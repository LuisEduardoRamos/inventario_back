using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace InventarioApi.Models{
    public class DetallePedido{
        [Key]
        public int id {get; set;}

        [ForeignKey("Pedido")]
        public int Pedido {get; set;}

        [ForeignKey("Producto")]
        public int Producto { get;set;}

        [ForeignKey("Inventario")]
        public int Inventario { get;set;}
        
        public int Cantidad { get; set;}

    }
}
