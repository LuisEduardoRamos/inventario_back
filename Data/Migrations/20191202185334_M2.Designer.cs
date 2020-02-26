﻿// <auto-generated />
using InventarioApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InventarioApi.Data.Migrations
{
    [DbContext(typeof(InventarioContext))]
    [Migration("20191202185334_M2")]
    partial class M2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InventarioApi.Models.CategoriaProducto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Restricciones")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("id");

                    b.ToTable("CategoriaProducto");
                });

            modelBuilder.Entity("InventarioApi.Models.DetallePedido", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Inventario")
                        .HasColumnType("int");

                    b.Property<int>("Pedido")
                        .HasColumnType("int");

                    b.Property<int>("Producto")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("DetallePedido");
                });

            modelBuilder.Entity("InventarioApi.Models.Inventario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("FechaIngreso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Lote")
                        .HasColumnType("int");

                    b.Property<int>("Producto")
                        .HasColumnType("int");

                    b.Property<int>("Sucursal")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("InventarioApi.Models.Lote", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FechaCaducidad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaMaxima")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Lote");
                });

            modelBuilder.Entity("InventarioApi.Models.Pedido", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaCreacion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaEntrega")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sucursal")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("InventarioApi.Models.Producto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaProducto")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<int>("Proveedor")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("InventarioApi.Models.Proveedor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacto")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("id");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("InventarioApi.Models.Sucursal", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacto")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("Latitud")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Longitud")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("InventarioApi.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Sucursal")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
