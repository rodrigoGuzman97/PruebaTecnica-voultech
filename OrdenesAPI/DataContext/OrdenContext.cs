using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrdenesAPI.Models;

namespace OrdenesAPI.DataContext;

public partial class OrdenContext : DbContext
{
    public OrdenContext()
    {
    }

    public OrdenContext(DbContextOptions<OrdenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }

    public virtual DbSet<OrdenProducto> OrdenProductos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Orden;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdenCompra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenCom__3214EC27BB71EB55");

            entity.ToTable("OrdenCompra");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cliente).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<OrdenProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdenPro__3214EC272FBF5B16");

            entity.ToTable("OrdenProducto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OrdenCompraId).HasColumnName("OrdenCompraID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.OrdenCompra).WithMany(p => p.OrdenProductos)
                .HasForeignKey(d => d.OrdenCompraId)
                .HasConstraintName("FK_OrdenProducto_Orden");

            entity.HasOne(d => d.Producto).WithMany(p => p.OrdenProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrdenProducto_Producto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC272E2B8BA3");

            entity.ToTable("Producto");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(200);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
