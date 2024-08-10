using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DL;

public partial class InnotecContext : DbContext
{
    public InnotecContext()
    {
    }

    public InnotecContext(DbContextOptions<InnotecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }
    public virtual DbSet<Compra> Compras { get; set; }
    public virtual DbSet<Departamento> Departamentos { get; set; }
    public virtual DbSet<Envio> Envios { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }
    public virtual DbSet<Proveedor> Proveedors { get; set; }
    public virtual DbSet<Subcategorium> Subcategoria { get; set; }
    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }
    public virtual DbSet<Pedido> Pedidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C7BC3F1F1");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Nombre).HasMaxLength(255);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Categoria__idDep__48CFD27E");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__48B99DB7B8E41685");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("FK_Compra_Producto");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.Idusuario)
                .HasConstraintName("FK_Compra_Usuario");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__C225F98D65B6DFC5");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.HasKey(e => e.IdEnvio).HasName("PK__Envio__527F831F3025D0A1");

            entity.ToTable("Envio");

            entity.Property(e => e.IdEnvio).HasColumnName("idEnvio");
            entity.Property(e => e.Calle).IsUnicode(false);
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Colonia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdPedido).HasColumnName("idPedido"); // Clave foránea a Pedido
            entity.Property(e => e.Municipio)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__Envio__idCompra__5441852A");

            entity.HasOne(d => d.Pedido)
                .WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Envio__idPedido__54556A49"); 
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__C711D41B");

            entity.ToTable("Pedido");

            entity.Property(e => e.IdPedido).HasColumnName("idPedido");
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.FechaPedido).HasColumnName("fechaPedido");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id"); 

            entity.HasOne(d => d.Compra)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__Pedido__idCompra__54656A49");


        });



        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProductos).HasName("PK__Producto__A26E462D078CD0F6");

            entity.Property(e => e.IdProductos).HasColumnName("idProductos");
            entity.Property(e => e.DescripcionDelProducto).IsUnicode(false);
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdDepartamento).HasColumnName("idDepartamento");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.IdSubcategoria).HasColumnName("idSubcategoria");
            entity.Property(e => e.ImagenDelProducto).HasColumnType("image");
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Productos_Categoria");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdDepartamento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idDep__412EB0B6");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__idPro__403A8C7D");

            entity.HasOne(d => d.IdSubcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdSubcategoria)
                .HasConstraintName("FK_Productos_Subcategoria");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6B2A9A7E56");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(15);
        });

        modelBuilder.Entity<Subcategorium>(entity =>
        {
            entity.HasKey(e => e.IdSubcategoria).HasName("PK__Subcateg__8EA50186C7E3C409");

            entity.Property(e => e.IdSubcategoria).HasColumnName("idSubcategoria");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Subcategoria)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subcatego__idCat__4BAC3F29");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipousuario).HasName("PK__TipoUsua__4B78286B1882E2F9");

            entity.ToTable("TipoUsuario");

            entity.Property(e => e.IdTipousuario).HasColumnName("idTipousuario");
            entity.Property(e => e.TipoUsuario1)
                .HasMaxLength(255)
                .HasColumnName("TipoUsuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuario__2ED7D2AF15CF3493");

            entity.ToTable("usuario");

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(45);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(45);
            entity.Property(e => e.Celular).HasMaxLength(15);
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FotoDePerfil).HasColumnType("image");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.Sexo).HasMaxLength(10);
            entity.Property(e => e.Telefono).HasMaxLength(15);
            entity.Property(e => e.TipoUsuarioIdTipousuario).HasColumnName("TipoUsuario_idTipousuario");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.TipoUsuarioIdTipousuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.TipoUsuarioIdTipousuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__TipoUsu__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
