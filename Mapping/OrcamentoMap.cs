using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Colex.Mapping
{
    public class OrcamentoMap : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.ToTable("Orcamento");

            builder.HasKey(o => o.IdOrcamento);
            builder.Property(o => o.Cliente).HasMaxLength(100);
            builder.Property(o => o.Cep).HasMaxLength(40); 
            builder.Property(o => o.Endereco).HasMaxLength(300);
            builder.Property(o => o.Cidade).HasMaxLength(100);
            builder.Property(o => o.Bairro).HasMaxLength(100);
            builder.Property(o => o.Cnpj).HasMaxLength(100);
            builder.Property(o => o.Cpf).HasMaxLength(100);
            builder.Property(o => o.Email).HasMaxLength(300);
            builder.Property(o => o.Estado).HasMaxLength(100);
            builder.Property(o => o.Comprador).HasMaxLength(100);
            builder.Property(o => o.Vendedor).HasMaxLength(100);
            builder.Property(o => o.Telefone).HasMaxLength(100);
            builder.Property(o => o.DataCriacao).HasColumnType("date");
            builder.Property(o => o.DataModificacao).HasColumnType("date");
            builder.Property(o => o.Nota).HasMaxLength(500);
            builder.Property(o => o.Observacao).HasMaxLength(500);
            builder.Property(o => o.ValorFinal).HasColumnType("decimal");

        }
    }
}
