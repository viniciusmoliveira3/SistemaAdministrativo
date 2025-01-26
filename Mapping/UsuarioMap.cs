using Colex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colex.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.Nome).HasMaxLength(100).IsRequired();
            builder.Property(u => u.CPF).HasColumnType("bigint").IsRequired();
            builder.Property(u => u.Login).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Senha).HasMaxLength(10).IsRequired();
            builder.Property(u => u.Ativo).HasColumnType("bool");
        }
    }
}
