using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Data;
using InventarioApi.Models;

namespace InventarioApi.Controllers
{
    [Route("api/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly InventarioContext _context;

        public CategoriaController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaProducto>>> GetCategoriasProductos()
        {
            return await _context.CategoriasProductos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetCategoriaProducto(int id)
        {
            var categoriaProducto = await _context.CategoriasProductos.FindAsync(id);

            if (categoriaProducto == null)
            {
                return new JsonResult( new { mensaje = "No se encontró ninguna categoría con ese id."});
            }

            return new JsonResult(categoriaProducto);
        }

       
        [HttpPut("{id}")]
        public async Task<JsonResult> PutCategoriaProducto(int id, CategoriaProducto categoriaProducto)
        {
            if (id != categoriaProducto.id)
            {
                return new JsonResult(new {mensaje="No coinciden los id."});
            }

            _context.Entry(categoriaProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaProductoExists(id))
                {
                return new JsonResult(new {mensaje="No se encontro esa categoria"});
                }
                else
                {
                    throw;
                }
            }

                return new JsonResult(categoriaProducto);
        }
        [HttpPost]
        public async Task<ActionResult<CategoriaProducto>> PostCategoriaProducto(CategoriaProducto categoriaProducto)
        {
            _context.CategoriasProductos.Add(categoriaProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaProducto", new { id = categoriaProducto.id }, categoriaProducto);
        }
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCategoriaProducto(int id)
        {
            try {

            
            var categoriaProducto = await _context.CategoriasProductos.FindAsync(id);
            if (categoriaProducto == null)
            {
                return new JsonResult( new { mensaje = "No se encontró ninguna categoria con ese id."});
            }

            _context.CategoriasProductos.Remove(categoriaProducto);
            await _context.SaveChangesAsync();

            return new JsonResult( new { mensaje = "Se borro con éxito la categória"});
            }
            catch
            {
                return new JsonResult( new { mensaje = "La categoría se encuentra en uso. No es posible borrarla"});
            }
        }

        private bool CategoriaProductoExists(int id)
        {
            return _context.CategoriasProductos.Any(e => e.id == id);
        }
    }
}
