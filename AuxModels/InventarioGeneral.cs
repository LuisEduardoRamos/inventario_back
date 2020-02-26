using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class InventarioGeneral{
        public int id  {get; set;}
        public string Nombre { get;  set;}
        public int Total {get; set;}
            
    }
}