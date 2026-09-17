using DominoPontaDeQuina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DominoPontaDeQuina.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DominoPontaDeQuina.Domain.Interfaces;

namespace DominoPontaDeQuina.Infrastructure.Persistence.EntityConfigurations;

/// <summary>Configura o mapeamento de uma participação em partida.</summary>
public sealed class ParticipacaoPartidaConfiguration : IEntityTypeConfiguration<ParticipacaoPartida>
{
    /// <summary>Aplica chave composta, FKs implícitas e regras de exclusão.</summary>
    public void Configure(EntityTypeBuilder<ParticipacaoPartida> builder)
    {
        builder.ToTable("ParticipacoesPartida");
        builder.Property<Guid>("PartidaId");
        builder.Property<Guid>("JogadorId");
        builder.HasKey("PartidaId", "JogadorId");
        builder.Property(participacao => participacao.Pontos).IsRequired();
        builder.HasOne(p => p.Partida)
       .WithMany()
       .HasForeignKey("PartidaId");

        builder.HasOne(p => p.Jogador)
               .WithMany()
               .HasForeignKey("JogadorId");
    }
}
