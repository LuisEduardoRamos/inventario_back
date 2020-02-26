using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class PedidoObj
    {
        public int idSucursalOrigen { get; set; }
        public int idSucursalDestino { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaEntrega { get; set; }

        public string Estado { get; set; }
        public List<ProductoPedido> productos { get; set;}

    }
}
