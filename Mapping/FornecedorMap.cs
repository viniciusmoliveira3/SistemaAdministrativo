using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("Fornecedor");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.NomeFantasia).HasMaxLength(80).IsRequired();
            builder.Property(f => f.RazaoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(f => f.EnderecoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(f => f.CEP).HasColumnType("bigint");
            builder.Property(c => c.Bairro).HasMaxLength(40).IsRequired(false);
            builder.Property(c => c.Numero).HasColumnType("int");
            builder.Property(f => f.Cidade).HasMaxLength(80).IsRequired(false);
            builder.Property(f => f.Uf).HasMaxLength(2).IsRequired(false);
            builder.Property(f => f.CNPJ).HasColumnType("bigint");
            builder.Property(f => f.InscricaoEstadual).HasColumnType("bigint");
            builder.Property(f => f.Email).HasMaxLength(80).IsRequired(false);
            builder.Property(f => f.Telefone).HasColumnType("bigint");
            builder.Property(s => s.Ativo).HasColumnType("bool").IsRequired();



        }
    }
    
    
}
