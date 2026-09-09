using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.WebApi.Contracts;

// Por que não devolver as entidades (Partida, Jogador, ...) direto da API?
// 1) Elas têm coleções de navegação que se referenciam em ciclo
//    (Partida -> Lances -> Lance.Partida -> ...), o que quebra a serialização JSON.
// 2) Expor a entidade do domínio "engessa" a API ao modelo de dados interno.
// Por isso cada entidade tem um DTO de resposta correspondente, com um método
// FromEntity(...) que faz a conversão.

/// <summary>Representação de uma partida devolvida pela API.</summary>
public sealed record PartidaResponse(Guid Id, int PontuacaoAlvo, string Status, DateTime CriadaEm)
{
    public static PartidaResponse FromEntity(Partida partida) =>
        new(partida.Id, partida.PontuacaoAlvo, partida.Status, partida.CriadaEm);
}

/// <summary>Representação de um jogador devolvida pela API.</summary>
public sealed record JogadorResponse(Guid Id, string Nome)
{
    public static JogadorResponse FromEntity(Jogador jogador) =>
        new(jogador.Id, jogador.Nome);
}

/// <summary>Representação de um lance devolvida pela API.</summary>
public sealed record LanceResponse(Guid Id, DateTime Timestamp, Guid PartidaId, Guid JogadorId)
{
    // Note que aqui NÃO usamos lance.Partida / lance.Jogador: o PartidaService registra o
    // lance usando só os IDs (propriedades de FK "sombra" no EF Core), então essas
    // propriedades de navegação vêm nulas do serviço. Por isso recebemos os IDs à parte,
    // que já temos na rota do controller.
    public static LanceResponse FromEntity(Lance lance, Guid partidaId, Guid jogadorId) =>
        new(lance.Id, lance.Timestamp, partidaId, jogadorId);
}

/// <summary>Representação de uma posição do ranking devolvida pela API.</summary>
public sealed record RankingResponse(Guid JogadorId, string JogadorNome, int Vitorias)
{
    public static RankingResponse FromEntity(Ranking ranking) =>
        new(ranking.Jogador.Id, ranking.Jogador.Nome, ranking.Vitorias);
}
