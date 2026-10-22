using DominoPontaDeQuina.Core.Enums;
using DominoPontaDeQuina.Core.Interfaces;
using System.Collections.ObjectModel;

namespace DominoPontaDeQuina.Core.Models;

/// <summary>
/// Representa o nivel de partida na hierarquia Partida -> Rodadas -> Jogadas.
/// Neste nivel ficam o estado global, os times participantes e o historico das rodadas.
/// </summary>
/// <param name="pontuacaoAlvo">
/// Define a pontuacao minima que um time deve atingir para encerrar a partida como vencedor.
/// </param>
public class Partida(int pontuacaoAlvo = 50) : IPartida
{
    // Adicione estas duas linhas:
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// Armazena as rodadas registradas neste nivel da hierarquia.
    /// </summary>
    Stack<Rodada> _rodadas = [];

    /// <inheritdoc />
    public int PontuacaoAlvo { get; } = pontuacaoAlvo;

    /// <inheritdoc />
    public StatusPartida Status { get; protected set; } = StatusPartida.NaoIniciada;

    /// <inheritdoc />
    public List<Time> Times { get; } = [];

    /// <inheritdoc />
    public ReadOnlyCollection<Rodada> HistoricoRodadas => _rodadas.ToList().AsReadOnly();

    /// <inheritdoc />
    public Rodada? RodadaAtual => _rodadas?.Peek();

    /// <inheritdoc />
    /// <inheritdoc />
    public Dictionary<Time, int> GetPontuacaoTimes()
    {
        var pontuacoes = new Dictionary<Time, int>();

        // Itera sobre a lista de Times existente na classe Partida
        foreach (var time in Times)
        {
            // Assumindo que a classe Time tem a propriedade Pontuacao.
            // (Se a pontuação estiver guardada dentro de cada "Rodada", avise-me e ajustamos!)
            pontuacoes[time] = time.Pontuacao;
        }

        return pontuacoes;
    }

    /// <inheritdoc />
    public Time? GetTimeVencedor()
    {
        var pontuacoes = GetPontuacaoTimes();

        foreach (var par in pontuacoes)
        {
            if (par.Value >= PontuacaoAlvo)
            {
                return par.Key; // Retorna o time que atingiu ou passou do alvo
            }
        }

        return null; // Ninguém venceu ainda
    }

    /// <inheritdoc />
    public bool VerificaPontuacaoAlvoAtingida()
    {
        var pontuacoes = GetPontuacaoTimes();

        // Retorna true se ALGUM (Any) time tiver pontuação maior ou igual ao alvo
        return pontuacoes.Any(p => p.Value >= PontuacaoAlvo);
    }

    /// <inheritdoc />
    /// 
    /// <inheritdoc />
    public void IniciarNovaRodada()
    {
        if (Status is StatusPartida.Finalizada)
            throw new InvalidOperationException("Não é possível iniciar uma nova rodada em uma partida finalizada.");

        Status = StatusPartida.EmAndamento;
        _rodadas.Push(new());
    }
    public void FinalizarPartida()
    {
        // Validação usando uma exceção do seu próprio projeto (Namespace começando com DominoPontaDeQuina)
        
        if (VerificaPontuacaoAlvoAtingida())
        {
            Status = StatusPartida.Finalizada;
        }
    }
}