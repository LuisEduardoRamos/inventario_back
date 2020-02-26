using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class ProductoPedido
    {
        public int idProducto { get; set; }
        public int Cantidad { get; set; }
        
    }
}
