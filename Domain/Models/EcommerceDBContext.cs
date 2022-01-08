using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Domain.Models
{
    public class EcommerceDBContext : DbContext
    {
        public EcommerceDBContext()
        {
        }

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<ProductoCategorias> ProductoCategorias { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Transacciones> Transacciones { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Name=ConnectionDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.ToTable("categorias");

                entity.HasIndex(e => e.Nombre, "IX_categorias_nombre")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre, "idx_nombre_categoria");

                entity.HasIndex(e => e.ParentId, "idx_parent_id_categorias");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre");

                entity.Property(e => e.ParentId)
                    .HasColumnType("integer")
                    .HasColumnName("parent_id");
            });

            modelBuilder.Entity<Imagenes>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("imagenes");

                entity.Property(e => e.ProductoId)
                    .HasColumnType("integer")
                    .HasColumnName("producto_id");

                entity.Property(e => e.RutaImagen)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("ruta_imagen");

                entity.HasOne(d => d.Producto)
                    .WithMany()
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Pedidos>(entity =>
            {
                entity.ToTable("pedidos");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("integer")
                    .HasColumnName("cantidad");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.Precio)
                    .IsRequired()
                    .HasColumnType("numeric")
                    .HasColumnName("precio");

                entity.Property(e => e.ProductoId)
                    .HasColumnType("integer")
                    .HasColumnName("producto_id");

                entity.Property(e => e.Subtotal)
                    .IsRequired()
                    .HasColumnType("numeric")
                    .HasColumnName("subtotal");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.UsuarioId)
                    .HasColumnType("integer")
                    .HasColumnName("usuario_id");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProductoCategorias>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("producto_categorias");

                entity.Property(e => e.CategoriaId)
                    .HasColumnType("integer")
                    .HasColumnName("categoria_id");

                entity.Property(e => e.ProductoId)
                    .HasColumnType("integer")
                    .HasColumnName("producto_id");

                entity.HasOne(d => d.Categoria)
                    .WithMany()
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Producto)
                    .WithMany()
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.ToTable("productos");

                entity.HasIndex(e => e.Sku, "IX_productos_sku")
                    .IsUnique();

                entity.HasIndex(e => e.PrecioNormal, "idx_precio_producto");

                entity.HasIndex(e => e.Sku, "idx_sku_producto");

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Cantidad)
                    .HasColumnType("integer")
                    .HasColumnName("cantidad")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.CreatedAt)
                    .IsRequired()
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.Descripcion)
                    .HasColumnType("text")
                    .HasColumnName("descripcion");

                entity.Property(e => e.DescuentoPrecio)
                    .HasColumnType("numeric")
                    .HasColumnName("descuento_precio")
                    .HasDefaultValueSql("0.0");

                entity.Property(e => e.Disponible)
                    .HasColumnName("disponible")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.ImagenPrincipal)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("imagen_principal");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre");

                entity.Property(e => e.PrecioNormal)
                    .HasColumnType("numeric")
                    .HasColumnName("precio_normal")
                    .HasDefaultValueSql("0.0");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("sku");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("current_timestamp");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.ToTable("roles");

                entity.HasIndex(e => e.Nombre, "IX_roles_nombre")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Transacciones>(entity =>
            {
                entity.ToTable("transacciones");

                entity.HasIndex(e => e.Codigo, "IX_transacciones_codigo")
                    .IsUnique();

                entity.HasIndex(e => e.PedidoId, "IX_transacciones_pedido_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("codigo");

                entity.Property(e => e.FechaTransaccion)
                    .HasColumnName("fecha_transaccion")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.Monto)
                    .IsRequired()
                    .HasColumnType("numeric")
                    .HasColumnName("monto");

                entity.Property(e => e.NumeroTarjeta)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasColumnName("numero_tarjeta");

                entity.Property(e => e.PedidoId)
                    .HasColumnType("integer")
                    .HasColumnName("pedido_id");

                entity.HasOne(d => d.Pedido)
                    .WithOne(p => p.Transacciones)
                    .HasForeignKey<Transacciones>(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Email, "IX_usuarios_email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("integer")
                    .ValueGeneratedNever()
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Active)
                    .HasColumnType("bool")
                    .HasColumnName("active")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("apellido");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasColumnName("password");
                
                entity.Property(e => e.RolId)
                    .HasColumnType("integer")
                    .HasColumnName("rol_id");
                
                entity.Property(e => e.RegisteredAt)
                    .IsRequired()
                    .HasColumnName("registered_at")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.Thumbnail)
                    .HasColumnType("varchar(300)")
                    .HasColumnName("thumbnail")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UpdatedAt)
                    .IsRequired()
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("current_timestamp");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
