using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class ComponenteMap : IEntityTypeConfiguration<Componente>
    {
        public void Configure(EntityTypeBuilder<Componente> builder)
        {
            builder.ToTable("Componente");

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Fornecedor).WithMany().HasForeignKey(c => c.IdFornecedor).IsRequired();
            builder.Property(c => c.Lote).HasMaxLength(10).IsRequired(false);
            builder.Property(c => c.Data).HasColumnType("date");
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Quantidade).HasColumnType("real");  
            builder.Property(c => c.QuantidadeAtual).HasColumnType("real");  
            builder.Property(c => c.Certificado).HasMaxLength(100).IsRequired(false);
            builder.Property(c => c.Ativo).HasColumnType("bool");
            builder.Property(c => c.NF).HasColumnType("bigint");


        } 

    }
    
    
}
