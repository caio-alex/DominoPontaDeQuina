using DominoPontaDeQuina.Core.Models;

namespace DominoPontaDeQuina.Domain.Interfaces;

public interface IJogadorRepository
{
    Task<Jogador> AdicionarComUsuarioAsync(Jogador jogador, Guid? usuarioId = null, CancellationToken cancellationToken = default);
    Task<bool> ExisteAsync(Guid id, CancellationToken cancellationToken = default);
}