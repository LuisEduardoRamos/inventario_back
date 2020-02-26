using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace InventarioApi.Models{
    public class Inventario{
        
        [Key]
        public int id {get; set;}
       
        [ForeignKey("Producto")]
        public int Producto { get;  set;}
        
        [ForeignKey("Sucursal")]
        public int Sucursal {get; set;}
        
        public string FechaIngreso {get; set;}
        
        public int Cantidad {get; set;}
        
        [ForeignKey("Lote")]
        public int Lote {get; set;}
       
        public string Comentarios {get; set;}
    }
}


// id INT IDENTITY PRIMARY KEY NOT NULL, 
// nProducto INT NOT NULL FOREIGN KEY REFERENCES Producto(id), 
//nSucursal INT NOT NULL FOREIGN KEY REFERENCES Sucursal(id), 
//dFechaIngreso DATE  NOT NULL, 
// nCantidad INT NOT NULL, 
// nlote INT NULL FOREIGN KEY REFERENCES Lote(id), 
// cComentarios VARCHAR(250));

