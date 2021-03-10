using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("uuid_generate_v1()");

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(x => x.Senha)
                .HasMaxLength(255)
                .IsRequired(false);
        }
    }
}
