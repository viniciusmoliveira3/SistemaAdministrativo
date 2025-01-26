using Colex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class RelatorioItensMap : IEntityTypeConfiguration<RelatorioItens>
    { 
        public void Configure(EntityTypeBuilder<RelatorioItens> builder) 
        {

            builder.ToTable("RelatorioItens");

            builder.HasKey(r => r.IdRelatorioItens);
            builder.HasOne(r => r.Os).WithMany().HasForeignKey(r => r.IdOServico);
            builder.HasOne(r => r.Extintor).WithMany().HasForeignKey(r => r.IdExtintor);
            builder.HasOne(r => r.Componente1).WithMany().HasForeignKey(r => r.IdComponentes1).IsRequired(false);
            builder.HasOne(r => r.Componente2).WithMany().HasForeignKey(r => r.IdComponentes2).IsRequired(false);   
            builder.HasOne(r => r.Componente3).WithMany().HasForeignKey(r => r.IdComponentes3).IsRequired(false);
            builder.HasOne(r => r.Componente4).WithMany().HasForeignKey(r => r.IdComponentes4).IsRequired(false);
            builder.Property(r => r.DataProximaRecarga).HasColumnType("date");
            builder.Property(o => o.NivelManutencao).HasColumnType("int").IsRequired();
            builder.Property(o => o.EnsaioIndPre).HasColumnType("bool");
            builder.Property(o => o.EnsaioVazVal).HasColumnType("bool");
            builder.Property(o => o.Reaproveitado).HasColumnType("bool");
            builder.Property(o => o.InspRosca).HasColumnType("bool");
            builder.Property(o => o.VisualIntacto).HasColumnType("bool");
            builder.Property(o => o.Pintura).HasColumnType("bool");
            builder.Property(o => o.PesoCilindroVazio).HasColumnType("real");
            builder.Property(o => o.PesoComAgua).HasColumnType("real");
            builder.Property(o => o.VolumeLitros).HasColumnType("real");
            builder.Property(o => o.CapacidadeMaxima).HasColumnType("real");
            builder.Property(o => o.PressaoTrabalho).HasColumnType("real");
            builder.Property(o => o.PressaoTesteCilindro).HasColumnType("real");
            builder.Property(o => o.PressaoTesteValvula).HasColumnType("real");
            builder.Property(o => o.PressaoTesteMangueira).HasColumnType("real");
            builder.Property(o => o.PressaoTesteManometro).HasColumnType("real");
            builder.Property(o => o.DefInstantanea).HasColumnType("real");
            builder.Property(o => o.DefPermanente).HasColumnType("real");
            builder.Property(o => o.PorcEpEt).HasColumnType("real");
            builder.Property(o => o.TaraGravada).HasColumnType("real");
            builder.Property(o => o.TaraReal).HasColumnType("real");
            builder.Property(o => o.PerdaMassa).HasColumnType("real");
            builder.Property(o => o.MotivoRepro).HasColumnType("int");
            builder.Property(o => o.LaudoAR).HasMaxLength(10).IsRequired(false);
            builder.Property(o => o.NumSelo).HasColumnType("bigint").IsRequired(false);



        }


    }
}
