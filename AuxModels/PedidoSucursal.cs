using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class PedidoSucursal
    {
        public int id { get; set; }
        public Sucursal sucursalOrigen { get; set; }
        public Sucursal sucursalDestino  { get; set; }
        public string fechaCreacion { get; set; }
        public string fechaEntrega { get; set; }
        public string estado { get; set; }

    }
}
