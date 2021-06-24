using Hogwarts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hogwarts.Data.Mapping
{
    public class CharacterMapping : IEntityTypeConfiguration<CharacterEntity>
    {
        public void Configure(EntityTypeBuilder<CharacterEntity> builder)
        {
            builder.ToTable("Character");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.role)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.school)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.house)
                   .IsRequired()
                   .HasMaxLength(36);

            builder.Property(p => p.patronus)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
