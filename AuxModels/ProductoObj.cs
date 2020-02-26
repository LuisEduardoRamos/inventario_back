using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class ProductoObj
    {
        public int id { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set;}

        public Proveedor DetallesProveedor { get; set;}

        public CategoriaProducto DetallesCategoria { get; set;}
        
    }
}
