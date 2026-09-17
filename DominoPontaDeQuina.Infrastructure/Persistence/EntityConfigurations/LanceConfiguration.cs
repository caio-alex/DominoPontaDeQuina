using DominoPontaDeQuina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DominoPontaDeQuina.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DominoPontaDeQuina.Domain.Interfaces;

namespace DominoPontaDeQuina.Infrastructure.Persistence.EntityConfigurations;

/// <summary>Configura o mapeamento de <see cref="Lance"/>.</summary>
public sealed class LanceConfiguration : IEntityTypeConfiguration<Lance>
{
    /// <summary>Aplica as relações do lance com partida e jogador.</summary>
    public void Configure(EntityTypeBuilder<Lance> builder)
    {
        builder.ToTable("Lances");
        builder.HasKey(lance => lance.Id);
        builder.Property(lance => lance.Timestamp).IsRequired();

        builder.HasOne(lance => lance.Partida)
            .WithMany()
            .HasForeignKey("PartidaId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(lance => lance.Jogador)
            .WithMany()
            .HasForeignKey("JogadorId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex("PartidaId", "Timestamp");
    }
}
