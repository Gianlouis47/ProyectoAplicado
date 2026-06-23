using Microsoft.EntityFrameworkCore;
using GoldClub.Models;

namespace ProyectoAplicado.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<Ubicacion> Ubicaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("productos");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.UbicacionId).HasColumnName("ubicacion_id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.CodigoSku).HasColumnName("codigo_sku").HasMaxLength(50);
            entity.Property(e => e.Marca).HasColumnName("marca").HasMaxLength(100);
            entity.Property(e => e.Modelo).HasColumnName("modelo").HasMaxLength(100);
            entity.Property(e => e.PrecioCompra).HasColumnName("precio_compra").HasPrecision(10, 2);
            entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta").HasPrecision(10, 2);
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");
            entity.Property(e => e.UnidadMedida).HasColumnName("unidad_medida").HasMaxLength(20);
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
            entity.Property(e => e.UsuarioCreadorId).HasColumnName("usuario_creador_id");
            entity.Property(e => e.SkuAutogenerado).HasColumnName("sku_autogenerado");
            entity.Property(e => e.ModificadoEn).HasColumnName("modificado_en");
            entity.Property(e => e.CodigoBarras).HasColumnName("codigo_barras").HasMaxLength(50);
            entity.Property(e => e.Tags).HasColumnName("tags");

            entity.HasOne(e => e.Categoria)
                  .WithMany()
                  .HasForeignKey(e => e.CategoriaId);

            entity.HasOne(e => e.Proveedor)
                  .WithMany()
                  .HasForeignKey(e => e.ProveedorId);

            entity.HasOne(e => e.Ubicacion)
                  .WithMany()
                  .HasForeignKey(e => e.UbicacionId);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("categorias");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("proveedores");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreEmpresa).HasColumnName("nombre_empresa").HasMaxLength(150);
            entity.Property(e => e.ContactoNombre).HasColumnName("contacto_nombre").HasMaxLength(100);
            entity.Property(e => e.Telefono).HasColumnName("telefono").HasMaxLength(20);
            entity.Property(e => e.Correo).HasColumnName("correo").HasMaxLength(150);
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Rnc).HasColumnName("rnc").HasMaxLength(20);
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
        });

        modelBuilder.Entity<Ubicacion>(entity =>
        {
            entity.ToTable("ubicaciones");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CreadoEn).HasColumnName("creado_en");
        });
    }
}