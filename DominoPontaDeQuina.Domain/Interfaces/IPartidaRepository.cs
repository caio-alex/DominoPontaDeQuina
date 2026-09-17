using DominoPontaDeQuina.Core.Models;

namespace DominoPontaDeQuina.Domain.Interfaces;

public interface IPartidaRepository
{
    Task<Partida> AdicionarAsync(Partida partida, CancellationToken cancellationToken = default);
    Task<Partida?> ObterAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Partida>> ObterHistoricoAsync(CancellationToken cancellationToken = default);
}