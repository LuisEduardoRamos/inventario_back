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
    [Route("api/proveedor")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ProveedoresController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return new JsonResult(new { mensaje= "No se encontro el proveedor con ese id."});
            }

            return new JsonResult(proveedor);
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutProveedor(int id, Proveedor proveedor)
        {
            if (id != proveedor.id)
            {
                return new JsonResult( new { mensaje="El id del proveedor debe de coincidir con el del url."});
            }

            _context.Entry(proveedor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return new JsonResult(new {mensaje= "No se encontro ningún proveedor con ese id"});
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedor", new { id = proveedor.id }, proveedor);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteProveedor(int id)
        {
            try{
                var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return new JsonResult(new { mensaje = "No se encontró ningún proveedor con ese id"});
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return new JsonResult( new  {mensaje= "Se borro el proveedor con éxito"});
            } catch
            {
                return new JsonResult( new { mensaje = "El proveedor se encuentra en uso. No es posible borrarlo"});
            }
            
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedores.Any(e => e.id == id);
        }
    }
}
