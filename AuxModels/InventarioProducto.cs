using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class InventarioProducto{
        public int idProducto  {get; set;}
        public string nombreProducto { get;  set;}
        public int cantidad {get; set;}
            
    }
}
