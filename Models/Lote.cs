using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace InventarioApi.Models{
    public class Lote{
        [Key]
        public int id {get; set;}
        public string FechaMaxima {get; set;}
        public string FechaCaducidad {get; set;}
    }
}