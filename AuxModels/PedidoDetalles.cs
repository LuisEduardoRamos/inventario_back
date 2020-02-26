using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class PedidoDetalles
    {
        public int idPedido  { get; set; }
        public int idSucursal { get; set; }

        public string nombreSucursal { get; set; }

        public string fechaCreacion { get; set; }
        public string fechaEntrega { get; set; }
        public string estado { get; set; }

        

    }
}
