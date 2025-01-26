using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace Colex.Mapping
{
    public class OsMap : IEntityTypeConfiguration<Os>
    {
        public void Configure(EntityTypeBuilder<Os> builder)
        {
            builder.ToTable("Os");

            builder.HasKey(o => o.IdOServico);
            builder.HasOne(o => o.Representante).WithMany().HasForeignKey(o => o.IdRepresentante);
            builder.HasOne(o => o.Cliente).WithMany().HasForeignKey(o => o.IdCliente);
            builder.Property(o => o.DataAbertura).HasColumnType("date").IsRequired();
            builder.Property(o => o.NumeroOrdemServico).HasMaxLength(40);
            builder.Property(o => o.Ativo).HasColumnType("bool").IsRequired();


        }
    }
}
