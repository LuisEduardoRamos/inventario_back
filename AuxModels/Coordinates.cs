using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class Coordinates{
        public string latitud  {get; set;}
        public string longitud { get;  set;}
            
    }
}