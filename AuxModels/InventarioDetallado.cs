using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class InventarioDetallado{
        public int id  {get; set;}
        public  ProductoObj Producto  { get;  set;}
        public Sucursal Sucursal {get; set;}
        public string FechaIngreso {get; set;}
        public Lote Lote {get;set;}
        public string Comentarios {get;set;}
        public int Cantidad {get; set;}
            
    }
}