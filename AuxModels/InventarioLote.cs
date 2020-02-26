using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using InventarioApi.Models;

namespace InventarioApi.AuxModels{
    public class InventarioLote{
        public int id {get; set;}
        public string FechaMaxima {get; set;}
        public string FechaCaducidad {get; set;}
        public int Total {get; set;}

            
    }
}