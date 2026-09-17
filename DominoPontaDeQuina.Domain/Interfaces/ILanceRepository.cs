using DominoPontaDeQuina.Core.Models;

namespace DominoPontaDeQuina.Domain.Interfaces
{
    public interface ILanceRepository
    {
        Task<Lance> AdicionarNaPartidaAsync(Lance lance, Guid partidaId, Guid jogadorId, CancellationToken cancellationToken = default);
    }
}