using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Data;
using InventarioApi.Models;
using InventarioApi.AuxModels;

namespace InventarioApi.Controllers
{
    [Route("api/inventario")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioContext _context;

        public InventarioController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetInventarios()
        {

            var idSucursales = await _context.Sucursales.FromSqlRaw("SELECT * FROM Sucursal").ToListAsync();
            var response = new List<InventarioResponse>();
            foreach (Sucursal sucursal in idSucursales)
            {
                var productos = await _context.Inventarios.FromSqlInterpolated($"SELECT * FROM Inventario WHERE Sucursal ={sucursal.id}").ToListAsync();
                var infoProductos = new List<InventarioProducto>();
                foreach (Inventario prod in productos)
                {
                    var infoProd = await _context.Productos.FindAsync(prod.Producto);
                    var obj = new InventarioProducto();
                    obj.idProducto = infoProd.id;
                    obj.nombreProducto = infoProd.Nombre;
                    obj.cantidad = prod.Cantidad;
                    infoProductos.Add(obj);
                }
                var inv = new InventarioResponse();
                inv.idSucursal = sucursal.id;
                inv.nombreSucursal = sucursal.Nombre;
                inv.Productos = infoProductos;
                response.Add(inv);

            }
            return new JsonResult(response);


        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetInventario(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            var inv = new InventarioResponse();
            if (sucursal == null)
            {
                return new JsonResult(new { mensaje = "No se encontro ninguna sucursal con ese id" });
            }
            else
            {
                var productos = await _context.Inventarios.FromSqlInterpolated($"SELECT * FROM Inventario WHERE Sucursal ={sucursal.id}").ToListAsync();
                var infoProductos = new List<InventarioProducto>();
                foreach (Inventario prod in productos)
                {
                    Console.WriteLine($"---------- Producto id { prod.id } -----------------");
                    var infoProd = await _context.Productos.FindAsync(prod.Producto);
                    if (infoProd != null)
                    {
                        var obj = new InventarioProducto();
                        obj.idProducto = infoProd.id;
                        obj.nombreProducto = infoProd.Nombre;
                        obj.cantidad = prod.Cantidad;
                        infoProductos.Add(obj);
                    }

                }
                inv.idSucursal = sucursal.id;
                inv.nombreSucursal = sucursal.Nombre;
                inv.Productos = infoProductos;

            }

            return new JsonResult(inv);
        }


        [HttpPut("{id}")]
        public async Task<JsonResult> PutInventario(int id, Inventario inventario)
        {
            if (id != inventario.id)
            {
                return new JsonResult(new { mensaje = "El id del inventario debe coincidir con el id de l url." });
            }

            _context.Entry(inventario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
                {
                    return new JsonResult(new { mensaje = "No se encontró ningún inventario con ese id." });
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(inventario);
        }


        [HttpGet("inventarioGeneral")]
        public async Task<JsonResult> GetInventarioGeneral()
        {

            var lista = await _context.InventarioGeneral.FromSqlRaw("SELECT p.id, p.Nombre,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id GROUP BY p.Nombre,p.id ").ToListAsync();
            return new JsonResult(lista);

        }

        [HttpGet("inventarioGeneral/{id}")]
        public async Task<JsonResult> GetInventarioGeneral(int id)
        {

            var prod = await _context.Productos.FindAsync(id);

            if (prod == null)
            {
                return new JsonResult(new { mensaje = "No se encontro el producto con ese id en el inventario" });
            }

            var lista = await _context.InventarioGeneral.FromSqlInterpolated($"SELECT p.id, p.Nombre,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id WHERE p.id = {id} GROUP BY p.Nombre,p.id").FirstAsync();
            return new JsonResult(lista);

        }
        [HttpGet("inventarioCategoria")]
        public async Task<JsonResult> GetInventarioCategoria()
        {
            var lista = await _context.InventarioGeneral.FromSqlInterpolated($"SELECT p.CategoriaProducto as id ,c.Nombre,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id JOIN CategoriaProducto c ON p.CategoriaProducto = c.id GROUP BY p.CategoriaProducto, c.Nombre").ToListAsync();
            return new JsonResult(lista);

        }

        [HttpGet("inventarioCategoria/{id}")]
        public async Task<JsonResult> GetInventarioCategoria(int id)
        {
            var categoria = await _context.CategoriasProductos.FindAsync(id);
            if (categoria == null)
            {
                return new JsonResult(new { mensaje = "No se encontro la categoria con ese id en el inventario" });
            }
            var lista = await _context.InventarioGeneral.FromSqlInterpolated($"SELECT p.CategoriaProducto as id ,c.Nombre,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id JOIN CategoriaProducto c ON p.CategoriaProducto = c.id WHERE c.id ={id}  GROUP BY p.CategoriaProducto, c.Nombre").FirstAsync();
            return new JsonResult(lista);

        }

        [HttpGet("inventarioLote")]
        public async Task<JsonResult> GetInventarioLote()
        {
            var lista = await _context.InventarioLote.FromSqlInterpolated($"SELECT l.id,l.FechaMaxima,l.FechaCaducidad,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id JOIN Lote l ON l.id = i.Lote  GROUP BY l.id,l.FechaMaxima,l.FechaCaducidad").ToListAsync();
            return new JsonResult(lista);

        }
        [HttpGet("inventarioLote/{id}")]
        public async Task<JsonResult> GetInventarioLote(int id)
        {
            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                return new JsonResult(new { mensaje = "No se encontro el lote con ese id en el inventario" });
            }
            var lista = await _context.InventarioLote.FromSqlInterpolated($"SELECT l.id,l.FechaMaxima,l.FechaCaducidad,SUM(i.Cantidad) as Total  FROM Inventario i JOIN Producto p ON i.Producto = p.id JOIN Lote l ON l.id = i.Lote WHERE l.id ={id} GROUP BY l.id,l.FechaMaxima,l.FechaCaducidad").FirstAsync();
            return new JsonResult(lista);

        }
        [HttpGet("inventarioDetallado")]
        public async Task<JsonResult> GetInventarioDetallado()
        {
            var lista = new List<InventarioDetallado>();
            var inventario = await _context.Inventarios.ToListAsync();
            foreach( Inventario inv in inventario){
                var sucursal = await _context.Sucursales.FindAsync(inv.Sucursal);
                var producto = await _context.Productos.FindAsync(inv.Producto);
                var lote = await _context.Lotes.FindAsync(inv.Lote);
                if( producto != null || sucursal != null || lote != null ){
                    var proveedor = await _context.Proveedores.FindAsync(producto.Proveedor);
                    var categoria = await _context.CategoriasProductos.FindAsync(producto.CategoriaProducto);
                    var p = new ProductoObj();
                    var i = new InventarioDetallado();
                    p.id = producto.id;
                    p.Nombre = producto.Nombre;
                    p.DetallesProveedor = proveedor;
                    p.DetallesCategoria = categoria;
                    p.Descripcion = producto.Descripcion;
                    i.id = inv.id;
                    i.Sucursal = sucursal;
                    i.Producto = p;
                    i.FechaIngreso = inv.FechaIngreso;
                    i.Lote = lote;
                    i.Comentarios = inv.Comentarios;
                    i.Cantidad = inv.Cantidad;
                    lista.Add(i);
                }
                else{
                    return new JsonResult(new { mensaje = "Ocurrio un error"}); 
                }
            }            
            return new JsonResult(lista);

        }

        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(Inventario inventario)
        {
            _context.Inventarios.Add(inventario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventario", new { id = inventario.id }, inventario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Inventario>> DeleteInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return inventario;
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.id == id);
        }
    }
}


