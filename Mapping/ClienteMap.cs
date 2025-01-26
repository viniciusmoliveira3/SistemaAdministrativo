using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Representante).WithMany().HasForeignKey(c => c.IdRepresentante);
            builder.Property(c => c.NomeFantasia).HasMaxLength(80).IsRequired();
            builder.Property(c => c.RazaoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(c => c.EnderecoSocial).HasMaxLength(80).IsRequired(false);
            builder.Property(c => c.CEP).HasColumnType("bigint");
            builder.Property(c => c.Bairro).HasMaxLength(40).IsRequired(false);
            builder.Property(c => c.Numero).HasColumnType("bigint");
            builder.Property(c => c.Cidade).HasMaxLength(80).IsRequired(false);
            builder.Property(c => c.Uf).HasMaxLength(2).IsRequired(false);
            builder.Property(c => c.CNPJ).HasColumnType("bigint");
            builder.Property(c => c.InscricaoEstadual).HasColumnType("bigint");
            builder.Property(c => c.Email).HasMaxLength(80).IsRequired(false);
            builder.Property(c => c.Telefone).HasColumnType("bigint");
            builder.Property(s => s.Ativo).HasColumnType("bool");
        }
    }
}
