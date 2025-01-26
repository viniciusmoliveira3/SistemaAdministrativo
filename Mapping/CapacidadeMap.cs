using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class CapacidadeMap : IEntityTypeConfiguration<Capacidade>
    {
        public void Configure(EntityTypeBuilder<Capacidade> builder )
        {
            builder.ToTable("Capacidade");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.CapacidadeCarga).HasMaxLength(10).IsRequired();
        }
    
    }
}
