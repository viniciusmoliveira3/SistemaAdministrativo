using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class RepresentanteMap : IEntityTypeConfiguration<Representante> 
    {
        public void Configure(EntityTypeBuilder<Representante> builder)
        {
            builder.ToTable("Representante");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.NomeFantasia).HasMaxLength(80).IsRequired();
            builder.Property(r => r.RazaoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(r => r.EnderecoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(r => r.CEP).HasColumnType("bigint");
            builder.Property(c => c.Bairro).HasMaxLength(40).IsRequired(false);
            builder.Property(c => c.Numero).HasColumnType("int");
            builder.Property(r => r.Cidade).HasMaxLength(80).IsRequired(false);
            builder.Property(r => r.Uf).HasMaxLength(2).IsRequired(false);
            builder.Property(r => r.CNPJ).HasColumnType("bigint");
            builder.Property(r => r.InscricaoEstadual).HasColumnType("bigint");
            builder.Property(r => r.Email).HasMaxLength(80).IsRequired(false);
            builder.Property(r => r.Telefone).HasColumnType("bigint");
            builder.Property(s => s.Ativo).HasColumnType("bool").IsRequired();

        }
    }
}
