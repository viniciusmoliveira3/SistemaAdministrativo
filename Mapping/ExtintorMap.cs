using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class ExtintorMap : IEntityTypeConfiguration<Extintor>
    {
        public void Configure(EntityTypeBuilder<Extintor> builder)
        {
            builder.ToTable("Extintor");

            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.MateriaPrima).WithMany().HasForeignKey(e => e.IdMateriaPrima).IsRequired();
            builder.HasOne(e => e.MarcaExtintor).WithMany().HasForeignKey(e => e.IdMarcaExtintor).IsRequired();
            builder.HasOne(e => e.Capacidade).WithMany().HasForeignKey(e => e.IdCapacidade).IsRequired();
            builder.Property(e => e.NumeroCilindro).HasMaxLength(30).IsRequired();
            builder.Property(e => e.AnoFabricacao).HasColumnType("int").IsRequired();
            builder.Property(e => e.EnsaioHidrostatico).HasColumnType("int").IsRequired();
            builder.Property(e => e.ProximoEnsaioHisdrostatico).HasColumnType("int").IsRequired(); 
            builder.Property(e => e.NBR).HasMaxLength(15).IsRequired().IsRequired(false);
            builder.Property(e => e.Projeto).HasMaxLength(15).IsRequired(false);
            builder.Property(e => e.CapacExtintora).HasMaxLength(20).IsRequired(false);
            builder.Property(e => e.NumPatrimonio).HasMaxLength(30).IsRequired(false);
            builder.Property(e => e.Lote).HasMaxLength(10).IsRequired(false);
            builder.Property(s => s.Ativo).HasColumnType("bool").IsRequired();
            builder.Property(e => e.SeloAnterior).HasColumnType("bigint");

            builder.HasIndex(e => new { e.NumeroCilindro }).IsUnique();


        }

    }
}
