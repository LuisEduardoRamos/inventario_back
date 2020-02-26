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
    [Route("api/sucursal")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly InventarioContext _context;

        public SucursalesController(InventarioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sucursal>>> GetSucursales()
        {
            return await _context.Sucursales.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetSucursal(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);

            if (sucursal == null)
            {
                return new JsonResult(new { mensaje = "No se encontro niguna sucursal con ese id"});
            }

            return new JsonResult(sucursal);
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutSucursal(int id, Sucursal sucursal)
        {
            if (id != sucursal.id)
            {
                return new JsonResult(new {mensaje="El id de la url y el de la surcursal deben de coincidir"});
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalExists(id))
                {
                    return new JsonResult(new {mensaje="No existe ninguna sucursal con ese id"});
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(sucursal);
        }

        [HttpPost]
        public async Task<JsonResult> PostSucursal(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();

            return new JsonResult(sucursal);
        }
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteSucursal(int id)
        {
            try{
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal == null)
            {
                return new JsonResult(new { mensaje = "No se encontro ninguna sucursal con ese id."});
            }

            _context.Sucursales.Remove(sucursal);
            await _context.SaveChangesAsync();

            return new JsonResult( new { mensaje = "Se borro la sucursal con éxito"});
            }
            catch
            {
                return new JsonResult( new { mensaje = "La sucursal se encuentra en uso. No es posible borrarla"});
            }
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursales.Any(e => e.id == id);
        }
    }
}
