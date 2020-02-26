using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Password { get; set; }


    }
}
