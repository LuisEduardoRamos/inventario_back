using InventarioApi.Models;
using Microsoft.EntityFrameworkCore;
using InventarioApi.AuxModels;


namespace InventarioApi.Data{
    public class InventarioContext : DbContext{
        public InventarioContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder){
            
            builder.Entity<Sucursal>().Property(p => p.Nombre).HasMaxLength(100);
            builder.Entity<Sucursal>().Property(p => p.Direccion).HasMaxLength(150);
            builder.Entity<Sucursal>().Property(p => p.Contacto).HasMaxLength(100);
            builder.Entity<Sucursal>().Property(p => p.Latitud).HasMaxLength(10);
            builder.Entity<Sucursal>().Property(p => p.Longitud).HasMaxLength(10);

            builder.Entity<Proveedor>().Property(p => p.Nombre).HasMaxLength(120);
            builder.Entity<Proveedor>().Property(p => p.Contacto).HasMaxLength(100);
            builder.Entity<Proveedor>().Property(p => p.Telefono).HasMaxLength(15);

            builder.Entity<CategoriaProducto>().Property(p => p.Nombre).HasMaxLength(100);
            builder.Entity<CategoriaProducto>().Property(p => p.Restricciones).HasMaxLength(250);

            builder.Entity<Producto>().Property(p => p.Nombre).HasMaxLength(120);
            builder.Entity<Producto>().Property(p => p.Descripcion).HasMaxLength(200);

            builder.Entity<Usuario>().Property(p => p.Nombre).HasMaxLength(120);
            builder.Entity<Usuario>().Property(p => p.Correo).HasMaxLength(100);
            builder.Entity<Usuario>().Property(p => p.Telefono).HasMaxLength(15);
            builder.Entity<Usuario>().Property(p => p.NombreUsuario).HasMaxLength(30);
            builder.Entity<Usuario>().Property(p => p.Password).HasMaxLength(100);
            
            builder.Entity<Inventario>().Property(p => p.Comentarios).HasMaxLength(250);

            builder.Entity<Sucursal>().ToTable("Sucursal");
            builder.Entity<Proveedor>().ToTable("Proveedor");
            builder.Entity<CategoriaProducto>().ToTable("CategoriaProducto");
            builder.Entity<Producto>().ToTable("Producto");
            builder.Entity<Usuario>().ToTable("Usuario");
            builder.Entity<Inventario>().ToTable("Inventario");
            builder.Entity<Lote>().ToTable("Lote");
        }
        
        public DbSet<Sucursal> Sucursales {get; set;}
        public DbSet<Proveedor> Proveedores {get; set;}
        public DbSet<CategoriaProducto> CategoriasProductos {get; set;}
        public DbSet<Producto> Productos {get; set;}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<Inventario> Inventarios {get; set;}
        public DbSet<Lote> Lotes {get; set;}
        public DbSet<InventarioApi.Models.Pedido> Pedido { get; set; }
        public DbSet<DetallePedido> DetallePedido {get; set;}

        public DbSet<InventarioGeneral> InventarioGeneral { get; set;}
        public DbSet<InventarioLote> InventarioLote { get; set;}

    }
}