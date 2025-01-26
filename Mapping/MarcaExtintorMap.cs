using Colex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class MarcaExtintorMap : IEntityTypeConfiguration<MarcaExtintor>
    {
        public void Configure(EntityTypeBuilder<MarcaExtintor> builder)
        {
            builder.ToTable("MarcaExtintor");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Nome).HasMaxLength(20).IsRequired();
        }
    }
    
    
}
