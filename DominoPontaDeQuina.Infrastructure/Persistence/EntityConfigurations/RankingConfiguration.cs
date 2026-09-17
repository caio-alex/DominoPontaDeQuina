using DominoPontaDeQuina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DominoPontaDeQuina.Core.Models;
using DominoPontaDeQuina.Domain.Interfaces;

namespace DominoPontaDeQuina.Infrastructure.Persistence.EntityConfigurations;

/// <summary>Configura o mapeamento de <see cref="Ranking"/>.</summary>
public sealed class RankingConfiguration : IEntityTypeConfiguration<Ranking>
{
    /// <summary>Aplica a relação um-para-um entre ranking e jogador.</summary>
    public void Configure(EntityTypeBuilder<Ranking> builder)
    {
        builder.ToTable("Rankings");
        builder.Property<Guid>("JogadorId");
        builder.HasKey("JogadorId");
        builder.Property(ranking => ranking.Vitorias).IsRequired();

        builder.HasOne(r => r.Jogador)
       .WithMany()
       .HasForeignKey("JogadorId");
    }
}
