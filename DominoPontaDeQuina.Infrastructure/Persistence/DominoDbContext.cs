using DominoPontaDeQuina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using DominoPontaDeQuina.Domain.Interfaces;
using DominoPontaDeQuina.Core.Models;
namespace DominoPontaDeQuina.Infrastructure.Persistence;
using Jogador = DominoPontaDeQuina.Core.Models.Jogador;

/// <summary>Representa a sessão do EF Core com o banco do Dominó.</summary>
public class DominoDbContext(DbContextOptions<DominoDbContext> options) : DbContext(options)
{
    /// <summary>Obtém as contas de usuário persistidas.</summary>
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    /// <summary>Obtém as partidas persistidas.</summary>
    public DbSet<Partida> Partidas => Set<Partida>();
    /// <summary>Obtém os jogadores persistidos.</summary>
    public DbSet<DominoPontaDeQuina.Core.Models.Jogador> Jogadores { get; set; }
    /// <summary>Obtém as participações persistidas.</summary>
    public DbSet<ParticipacaoPartida> ParticipacoesPartida => Set<ParticipacaoPartida>();
    /// <summary>Obtém os lances persistidos.</summary>
    public DbSet<Lance> Lances => Set<Lance>();
    /// <summary>Obtém os rankings persistidos.</summary>
    public DbSet<Ranking> Rankings => Set<Ranking>();

    /// <summary>Aplica todas as configurações Fluent API da infraestrutura.</summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DominoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
