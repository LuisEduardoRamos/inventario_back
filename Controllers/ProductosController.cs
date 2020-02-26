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

namespace InventarioApi.Controllers
{
    [Route("api/producto")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProductosController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetProductos()
        {
            var listaProductos = new List<ProductoObj>();
            var productos = await _context.Productos.ToListAsync();

            foreach(Producto p in productos)
            {
                var proveedor = await _context.Proveedores.FindAsync(p.Proveedor);
                var categoria = await _context.CategoriasProductos.FindAsync(p.CategoriaProducto);
                var obj = new ProductoObj();
                obj.id = p.id;
                obj.Nombre =p.Nombre;
                obj.Descripcion = p.Descripcion;
                obj.DetallesProveedor = proveedor;
                obj.DetallesCategoria = categoria;
                listaProductos.Add(obj);
            }

            return new JsonResult(listaProductos);

        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            
            if (producto == null)
            {
                return new JsonResult( new { mensaje = "No se encontro ningún producto con ese id"});
            }
            var proveedor = await _context.Proveedores.FindAsync(producto.Proveedor);
            var categoria = await _context.CategoriasProductos.FindAsync(producto.CategoriaProducto);
            var prodObj = new ProductoObj();
            prodObj.id = producto.id;
            prodObj.Nombre = producto.Nombre;
            prodObj.Descripcion = producto.Descripcion;
            prodObj.DetallesProveedor = proveedor;
            prodObj.DetallesCategoria = categoria;
            return new JsonResult(prodObj);
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.id)
            {
                return new JsonResult( new { mensaje = "El id del producto debe de coincidir con el de la url."});
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return new JsonResult( new { mensaje = "El producto con ese id no existe."});
                }
                else
                {
                    return new JsonResult( new { mensaje = "Ocurrio un problema en la bd, vuelva a intentarlo"});
                }
            }

            return new JsonResult(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return new JsonResult( new { mensaje = "No se encontró ningún producto con ese id"});
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return new JsonResult(producto);
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.id == id);
        }
    }
}
