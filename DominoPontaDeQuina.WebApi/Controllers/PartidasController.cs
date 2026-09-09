using DominoPontaDeQuina.Application.Services;
using DominoPontaDeQuina.WebApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DominoPontaDeQuina.WebApi.Controllers;


[ApiController]
[Route("api/partidas")]
public sealed class PartidasController(IPartidaService partidaService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(PartidaResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PartidaResponse>> IniciarPartida(
        [FromBody] IniciarPartidaRequest? request,
        CancellationToken cancellationToken)
    {
        try
        {
            var partida = await partidaService.IniciarPartidaAsync(
                request?.PontuacaoAlvo ?? 50,
                cancellationToken);

            var response = PartidaResponse.FromEntity(partida);
            return CreatedAtAction(nameof(VerificarStatus), new { partidaId = response.Id }, response);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet("{partidaId:guid}")]
    [ProducesResponseType(typeof(PartidaResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PartidaResponse>> VerificarStatus(
        Guid partidaId,
        CancellationToken cancellationToken)
    {
        try
        {
            var partida = await partidaService.VerificarStatusAsync(partidaId, cancellationToken);
            return Ok(PartidaResponse.FromEntity(partida));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }

    /// <summary>Consulta o histórico de partidas.</summary>
    [HttpGet("historico")]
    [ProducesResponseType(typeof(IReadOnlyList<PartidaResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PartidaResponse>>> ConsultarHistorico(
        CancellationToken cancellationToken)
    {
        var historico = await partidaService.ConsultarHistoricoAsync(cancellationToken);
        return Ok(historico.Select(PartidaResponse.FromEntity));
    }

    /// <summary>Consulta o ranking de jogadores por vitórias.</summary>
    [HttpGet("ranking")]
    [ProducesResponseType(typeof(IReadOnlyList<RankingResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RankingResponse>>> ConsultarRanking(
        CancellationToken cancellationToken)
    {
        var ranking = await partidaService.ConsultarRankingAsync(cancellationToken);
        return Ok(ranking.Select(RankingResponse.FromEntity));
    }

    [HttpPost("{partidaId:guid}/jogadores")]
    [ProducesResponseType(typeof(JogadorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<JogadorResponse>> RegistrarJogador(
        Guid partidaId,
        [FromBody] RegistrarJogadorRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var jogador = await partidaService.RegistrarJogadorAsync(
                partidaId,
                request.Nome,
                request.UsuarioId,
                cancellationToken);

            return CreatedAtAction(nameof(VerificarStatus), new { partidaId }, JogadorResponse.FromEntity(jogador));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpPost("{partidaId:guid}/jogadores/{jogadorId:guid}/lances")]
    [ProducesResponseType(typeof(LanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LanceResponse>> RegistrarLance(
        Guid partidaId,
        Guid jogadorId,
        CancellationToken cancellationToken)
    {
        try
        {
            var lance = await partidaService.RegistrarLanceAsync(partidaId, jogadorId, cancellationToken);
            return Ok(LanceResponse.FromEntity(lance, partidaId, jogadorId));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { erro = ex.Message });
        }
    }
}
