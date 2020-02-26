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
    [Route("api/lote")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        private readonly InventarioContext _context;

        public LoteController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lote>>> GetLotes()
        {
            return await _context.Lotes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetLote(int id)
        {
            var lote = await _context.Lotes.FindAsync(id);

            if (lote == null)
            {
                return new JsonResult(new { mensaje = "No se encontró ningún lote con ese id."});
            }

            return new JsonResult(lote);
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutLote(int id, Lote lote)
        {
            if (id != lote.id)
            {
                return new JsonResult( new { mensaje = "El id del lote debe de coincidir con el del url."});
            }

            _context.Entry(lote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoteExists(id))
                {
                    return new JsonResult( new { mensaje = "No se encontró ningún lote con ese id."});
                }
                else
                {
                    return new JsonResult( new { mensaje = "Ocurrió un error en la bd,  vuelva a intentarlo"});
                }
            }

            return new JsonResult(lote);
        }

        [HttpPost]
        public async Task<ActionResult<Lote>> PostLote(Lote lote)
        {
            _context.Lotes.Add(lote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLote", new { id = lote.id }, lote);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteLote(int id)
        {
            try {
            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                return new JsonResult( new { mensaje = "No se encontró ningún lote con ese id"});
            }

            _context.Lotes.Remove(lote);
            await _context.SaveChangesAsync();

            return new JsonResult( new { mensaje = "El lote se ha borrado con éxito"});
            }
            catch
            {
                return new JsonResult( new { mensaje = "El Lote se encuentra en uso. No es posible borrarlo"});
            }

        }

        private bool LoteExists(int id)
        {
            return _context.Lotes.Any(e => e.id == id);
        }
    }
}
