namespace DominoPontaDeQuina.WebApi.Contracts;

/// <summary>Corpo da requisição para iniciar uma partida.</summary>
/// <param name="PontuacaoAlvo">
/// Pontuação necessária para vencer. Opcional: se omitido, o serviço usa o padrão (50).
/// </param>
public sealed record IniciarPartidaRequest(int? PontuacaoAlvo);

/// <summary>Corpo da requisição para registrar um jogador em uma partida.</summary>
/// <param name="Nome">Nome do jogador.</param>
/// <param name="UsuarioId">Conta de usuário associada ao jogador, quando houver.</param>
public sealed record RegistrarJogadorRequest(string Nome, Guid? UsuarioId);
