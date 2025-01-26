using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class MateriaPrimaMap : IEntityTypeConfiguration<MateriaPrima>
    {
        public void Configure(EntityTypeBuilder<MateriaPrima> builder)
        {
            builder.ToTable("MateriaPrima");

            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Fornecedor).WithMany().HasForeignKey(m => m.IdFornecedor).IsRequired(false);
            builder.Property(m => m.Nome).HasMaxLength(30).IsRequired();
            builder.Property(m => m.Quantidade).HasColumnType("real");
            builder.Property(m => m.QuantidadeAtual).HasColumnType("real");
            builder.Property(m => m.Data).HasColumnType("date");
            builder.Property(m => m.NF).HasMaxLength(30).IsRequired(false);
            builder.Property(m => m.Certificado).HasMaxLength(30).IsRequired(false);
            builder.Property(m => m.Batelada).HasMaxLength(30).IsRequired(false);
            builder.Property(m => m.LoteInterno).HasMaxLength(30).IsRequired(false);
            builder.Property(m => m.Ativo).HasColumnType("bool");
        }
    }
}
