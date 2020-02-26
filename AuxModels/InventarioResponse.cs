using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class InventarioResponse{
        public int idSucursal {get; set;}
        public string nombreSucursal { get;  set;}
        public List<InventarioProducto> Productos {get; set;}
            
    }
}
