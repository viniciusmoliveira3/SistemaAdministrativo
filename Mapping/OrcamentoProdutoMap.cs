using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class OrcamentoProdutoMap : IEntityTypeConfiguration<OrcamentoProduto>
    {
        public void Configure(EntityTypeBuilder<OrcamentoProduto> builder)
        {
            builder.ToTable("OrcamentoProduto");

            builder.HasKey(o => o.IdOrcamentoProduto);
            builder.HasOne(o => o.Orcamento).WithMany().HasForeignKey(o => o.IdOrcamento).IsRequired();
            builder.HasOne(o => o.MateriaPrima).WithMany().HasForeignKey(o => o.IdMateriaPrima).IsRequired(false);
            builder.Property(o => o.Quantidade).HasColumnType("int");
            builder.Property(o => o.ValorUnitario).HasColumnType("decimal");
            builder.Property(o => o.ValorTotal).HasColumnType("decimal");
        }

    }
}
