using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class SeloMap : IEntityTypeConfiguration<Selo>
    {
        public void Configure(EntityTypeBuilder<Selo> builder)
        {
            builder.ToTable("Selo");

            builder.HasKey(s => s.Id);
            builder.HasOne(s => s.Fornecedor).WithMany().HasForeignKey(s => s.IdFornecedor).IsRequired();
            builder.Property(s => s.Data).HasColumnType("date").IsRequired();
            builder.Property(s => s.NF).HasColumnType("bigint").IsRequired();
            builder.Property(s => s.NumeroFinal).HasColumnType("bigint").IsRequired();
            builder.Property(s => s.NumeroInicial).HasColumnType("bigint").IsRequired();
            builder.Property(s => s.Quantidade).HasColumnType("bigint").IsRequired();
            builder.Property(s => s.Ativo).HasColumnType("bool").IsRequired();


        }
    }
}
