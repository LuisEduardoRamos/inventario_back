using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace InventarioApi.Models{
    public class Pedido{
        [Key]
        public int id {get; set;}

        [ForeignKey("Sucursal")]
        public int SucursalOrigen {get; set;}
        
        [ForeignKey("Sucursal")]
        public int SucursalDestino {get; set;}

        public string  FechaCreacion {get; set;}
        public string  FechaEntrega {get; set;}
        public string  Estado {get; set;}
    }
}

