using DominoPontaDeQuina.Core.Models;

namespace DominoPontaDeQuina.Domain.Interfaces
{
    public interface IRankingRepository
    {
        Task<IReadOnlyList<Ranking>> ObterAsync(CancellationToken cancellationToken = default);
    }
}