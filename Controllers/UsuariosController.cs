using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioApi.Data;
using InventarioApi.Models;
using Microsoft.AspNetCore.Authorization;
using InventarioApi.AuxModels;

namespace InventarioApi.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly InventarioContext _context;

        public UsuariosController(InventarioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<JsonResult> GetUsuarios()
        {
            var listaUsuarios = new List<UsarioJson>();
            var usuarios = await _context.Usuarios.ToListAsync();
            foreach (Usuario u in usuarios)
            {
                var sucursal = await _context.Sucursales.FindAsync(u.Sucursal);
                var usr = new UsarioJson();
                usr.id = u.id;
                usr.Nombre = u.Nombre;
                usr.NombreUsuario = u.NombreUsuario;
                usr.Correo = u.Correo;
                usr.Password = u.Password;
                usr.Telefono = u.Telefono;
                usr.Sucursal = sucursal;
                listaUsuarios.Add(usr);
            }
            return new JsonResult(listaUsuarios);
        }

        [HttpPost("login")]
        public async Task<JsonResult> LoginUsuario(Login usuario)
        {
            Console.WriteLine("usuario: " + usuario.Usuario);
            Console.WriteLine($"SELECT * FROM Usuario WHERE NombreUsuario ='{usuario.Usuario}';");
            try
            {
                var usr = await _context.Usuarios.FromSqlInterpolated($"SELECT * FROM Usuario WHERE NombreUsuario ={usuario.Usuario}").FirstAsync();
                if (usr.Password != usuario.Password)
                {
                    return new JsonResult(new { mensaje = "Contraseña incorrecta." });
                }
                else
                {
                    return new JsonResult(usr);
                }
                
            }
            catch
            {
                return new JsonResult(new { mensaje = "Uusario incorrecto." });

            }
            

        }
        [HttpGet("{id}")]
        public async Task<JsonResult> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return new JsonResult(new { mensaje = "No se encontró ningún usuario con ese id." });
            }

            return new JsonResult(usuario);
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.id)
            {
                return new JsonResult(new { mensaje = "El id del usuario debe de coincidir con el id de la url." });
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return new JsonResult(new { mensaje = "No se encontro ningún usuario con ese id." });
                }
                else
                {
                    return new JsonResult(new { mensaje = "Ocurrio un error en la bd, vuelva a intentarlo" });
                }
            }

            return new JsonResult(usuario);
        }


        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.id }, usuario);
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return new JsonResult(new { mensaje = "No se encontró ningún usuario con ese id." });
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return new JsonResult(new { mensaje = "Se borró con exitó el usuario" });

        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.id == id);
        }
    }
}
