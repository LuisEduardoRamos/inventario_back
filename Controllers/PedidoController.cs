using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Data;
using InventarioApi.Models;
using InventarioApi.AuxModels;
using InventarioApi.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace InventarioApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly InventarioContext _context;
        private readonly IHubContext<InventarioHub> _hubContext;


        public PedidoController(InventarioContext context, IHubContext<InventarioHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<JsonResult> GetPedido()
        {
            var pedidos = await _context.Pedido.ToListAsync();
            var listaPedidos = new List<PedidoSucursal>();
            foreach (Pedido p in pedidos)
            {
                var sucursalOrigen = await _context.Sucursales.FindAsync(p.SucursalOrigen);
                var sucursalDestino = await _context.Sucursales.FindAsync(p.SucursalOrigen);
                var ped = new PedidoSucursal();
                ped.id = p.id;
                ped.sucursalDestino = sucursalDestino;
                ped.sucursalOrigen = sucursalOrigen;
                ped.fechaCreacion = p.FechaCreacion;
                ped.fechaEntrega = p.FechaEntrega;
                ped.estado = p.Estado;

                listaPedidos.Add(ped);
            }
            return new JsonResult(listaPedidos);
        }

        // GET: api/Pedido/5
        [HttpGet("detalles/{id}")]
        public async Task<JsonResult> getPedidoDetalles(int id)
        {
            var orden = await _context.Pedido.FindAsync(id);
            if (orden == null)
            {
                return new JsonResult(new { mensaje = "No se encontro ninguna orden con ese id" });
            }
            var sucursalOrigen = await _context.Sucursales.FindAsync(orden.SucursalOrigen);
            var sucursalDestino = await _context.Sucursales.FindAsync(orden.SucursalDestino);
            var ordenCompleta = new PedidoSucursal();
            ordenCompleta.id = orden.id;
            ordenCompleta.fechaEntrega = orden.FechaEntrega;
            ordenCompleta.fechaCreacion = orden.FechaCreacion;
            ordenCompleta.estado = orden.Estado;
            ordenCompleta.sucursalOrigen = sucursalOrigen;
            ordenCompleta.sucursalDestino = sucursalDestino;
            var productos = await _context.DetallePedido.FromSqlRaw($"SELECT * FROM DetallePedido WHERE Pedido={orden.id}").ToListAsync();
            if (productos == null)
            {
                return new JsonResult(new { mensaje = "No se encontrar con ese id." });
            }
            var listaProductos = new List<InventarioProducto>();
            foreach (DetallePedido p in productos)
            {
                var detalles = await _context.Productos.FindAsync(p.Producto);
                var prod = new InventarioProducto();
                prod.idProducto = p.Producto;
                prod.nombreProducto = detalles.Nombre;
                prod.cantidad = p.Cantidad;

                listaProductos.Add(prod);
            }
            return new JsonResult(new { PedidoDetalles = ordenCompleta, productos = listaProductos });
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<JsonResult> GetPedido(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null)
            {
                return new JsonResult(new { mensaje = "No se encontro ningún pedido con ese id." });
            }

            var sucursalOrigen = await _context.Sucursales.FindAsync(pedido.SucursalOrigen);
            var sucursalDestino = await _context.Sucursales.FindAsync(pedido.SucursalDestino);

            var ped = new PedidoSucursal();
            ped.id = pedido.id;
            ped.sucursalDestino = sucursalOrigen;
            ped.sucursalOrigen = sucursalDestino;
            ped.fechaCreacion = pedido.FechaCreacion;
            ped.fechaEntrega = pedido.FechaEntrega;
            ped.estado = pedido.Estado;

            return new JsonResult(ped);
        }

        // PUT: api/Pedido/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<JsonResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.id)
            {
                return new JsonResult(new { mensaje = "No se encontro nigún pedido con ese id" });
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return new JsonResult(new { mensaje = "No se encontro nigún pedido con ese id" });
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(pedido);
        }

        // POST: api/Pedido
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<JsonResult> PostPedido(PedidoObj pedido)
        {
            var pedidoNuevo = new Pedido();
            pedidoNuevo.SucursalDestino = pedido.idSucursalDestino;
            pedidoNuevo.SucursalOrigen = pedido.idSucursalOrigen;
            pedidoNuevo.FechaCreacion = pedido.FechaCreacion;
            pedidoNuevo.FechaEntrega = pedido.FechaEntrega;
            pedidoNuevo.Estado = "Creado";
            _context.Pedido.Add(pedidoNuevo);
            await _context.SaveChangesAsync();
            foreach (ProductoPedido p in pedido.productos)
            {
                var productoInventario = await _context.Inventarios.FromSqlInterpolated($"SELECT * FROM Inventario WHERE Sucursal = {pedido.idSucursalOrigen} AND Producto = {p.idProducto}").FirstAsync();
                if (productoInventario.Cantidad < p.Cantidad)
                {
                    return new JsonResult(new { mensaje = $"No existen las suficientes unidades del producto { p.idProducto}" });
                }
                else
                {
                    //Console.WriteLine($"UPDATE Inventario SET Cantidad = { productoInventario.Cantidad - p.Cantidad } WHERE Sucursal = {1} AND Producto = {p.idProducto}");
                    //_context.Inventarios.FromSqlInterpolated($"UPDATE Inventario SET Cantidad = { productoInventario.Cantidad - p.Cantidad } WHERE Sucursal = {1} AND Producto = {p.idProducto}");
                    var productoOrdenado = new DetallePedido();
                    productoOrdenado.Pedido = pedidoNuevo.id;
                    productoOrdenado.Producto = p.idProducto;
                    productoOrdenado.Inventario = productoInventario.id;
                    productoOrdenado.Cantidad = p.Cantidad;
                    _context.DetallePedido.Add(productoOrdenado);
                    Console.WriteLine($"El producto {p.idProducto} si hay en existencia");
                }

            }
            await _context.SaveChangesAsync();
            //Se tiene que modificar la bd para restar los productos seleccionados;
            // Console.WriteLine(pedido.Estado);
            // _context.Pedido.Add(pedido);

            // await _context.SaveChangesAsync();

            // return CreatedAtAction("GetPedido", new { id = pedido.id }, pedido);

            return new JsonResult(new { mensaje = $"Se guardo el pedido con el id {pedidoNuevo.id} " });
        }

        [HttpGet("salidas")]
        public async Task<JsonResult> GetProductosSalidas()
        {
            var lista = await _context.InventarioGeneral.FromSqlInterpolated($"SELECT p.id,p.Nombre as Nombre,SUM(d.Cantidad) Total FROM DetallePedido d JOIN Producto p ON p.id = d.Producto GROUP BY p.id,p.Nombre").ToListAsync();
            return new JsonResult(lista);

        }
        //Aqui debe de ir la implementación de signal R
        [HttpPost("localizacion/{id}")]
        public async Task<JsonResult> PostCoordinates(int id, Coordinates coordenada)
        {
            try
            {
                var pedido = await _context.Pedido.FindAsync(id);
                if (pedido == null)
                {
                    return new JsonResult(new {mensaje= "No se encontro ningún pedido con ese id"});
                }
                else
                {
                    await _hubContext.Clients.All.SendAsync("SendPosition",id, coordenada);
                    Console.WriteLine($"El pedido {id} tiene se encuentra en lat: {coordenada.latitud} y lon: {coordenada.longitud}");
                    return new JsonResult(new { mensaje = "Se actualizo la posición del pedido" });

                }
            }
            catch
            {
                return new JsonResult(new { mensaje = "Ocurrio un error, vuelve a intentar." });
            }

        }
        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return new JsonResult(new { mensaje = "No se econtro ningún pedido con ese id." });
            }

            _context.Pedido.Remove(pedido);
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine($"DELETE FROM DetallePedido WHERE Pedido={id}");
            _context.DetallePedido.FromSqlInterpolated($"DELETE FROM DetallePedido WHERE Pedido={id}");
            await _context.SaveChangesAsync();

            return new JsonResult(new { mensaje = "Se borro con exito el pedido" });
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedido.Any(e => e.id == id);
        }
    }
}

