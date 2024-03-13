using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LLVG20240312.Models
{
    public partial class LLVG20241103DBContext : DbContext
    {
        public LLVG20241103DBContext()
        {
        }

        public LLVG20241103DBContext(DbContextOptions<LLVG20241103DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<NumerosTelefono> NumerosTelefonos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__677F38F59A4B4F11");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("correo_electronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<NumerosTelefono>(entity =>
            {
                entity.HasKey(e => e.IdTelefono)
                    .HasName("PK__NumerosT__28CD6802C9AEDB1B");

                entity.Property(e => e.IdTelefono).HasColumnName("id_telefono");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.NumeroTelefono)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("numero_telefono");

                entity.Property(e => e.TipoTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_telefono");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.NumerosTelefono)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__NumerosTe__id_cl__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
