using DominoPontaDeQuina.Core.Models;

namespace DominoPontaDeQuina.Domain.Interfaces;

public interface IParticipacaoRepository
{
    Task AdicionarNaPartidaAsync(ParticipacaoPartida participacao, Guid partidaId, Guid jogadorId, CancellationToken cancellationToken = default);
}