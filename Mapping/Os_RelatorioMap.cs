using Colex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class Os_RelatorioMap : IEntityTypeConfiguration<Os_Relatorio>
    {
      public void Configure(EntityTypeBuilder<Os_Relatorio> builder)
      {
            builder.ToTable("Os_Relatorio");

            builder.HasKey(o => o.IdOsRelatorio);
            builder.HasOne(o => o.Os).WithMany().HasForeignKey(o => o.IdOServico).IsRequired();
            builder.HasOne(o => o.RelatorioItens).WithMany().HasForeignKey(o => o.IdRelatorioItens).IsRequired();
            builder.Property(o => o.Relatorio).HasMaxLength(256);





      } 
    }
}
