using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class UsarioJson
    {
        public int id { get; set; }
        public string Nombre { get; set; }

        public string Correo { get; set;}

        public string Telefono { get; set;}

        public string NombreUsuario { get; set;}
        
        public string Password { get; set;}
        public Sucursal Sucursal { get; set;}
        
    }
}
