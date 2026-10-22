using DominoPontaDeQuina.Core.Enums;

namespace DominoPontaDeQuina.Core.Models;

/// <summary>
/// Representa o tabuleiro no nivel da rodada dentro da hierarquia Partida -> Rodadas -> Jogadas.
/// Neste nivel ficam as pecas ja coladas e as informacoes necessarias para validar jogadas,
/// calcular pontuacao pelas pontas externas e verificar situacoes de travamento.
/// </summary>
public class Tabuleiro
{
    /// <summary>
    /// Obtem as pecas posicionadas no tabuleiro na ordem em que foram coladas.
    /// </summary>
    public List<Peca> Pecas { get; } = [];

    /// <summary>
    /// Indica se o tabuleiro ainda nao possui pecas coladas.
    /// </summary>
    public bool EstaVazio => Pecas.Count == 0;

    /// <summary>
    /// Obtem a ponta esquerda atualmente exposta no tabuleiro.
    /// Quando o tabuleiro estiver vazio, nao existe ponta externa disponivel.
    /// </summary>
    public int? PontaEsquerda => EstaVazio ? null : Pecas[0].ValorA;

    /// <summary>
    /// Obtem a ponta direita atualmente exposta no tabuleiro.
    /// Quando o tabuleiro estiver vazio, nao existe ponta externa disponivel.
    /// </summary>
    public int? PontaDireita => EstaVazio ? null : Pecas[^1].ValorB;

    /// <summary>
    /// Determina se uma peca pode ser colada no lado informado.
    /// A regra esperada e validar se a peca possui valor compativel com a ponta externa do lado escolhido.
    /// </summary>
    /// <param name="peca">A peca a ser verificada.</param>
    /// <param name="lado">O lado do tabuleiro.</param>
    /// <returns><see langword="true"/> quando a peca puder ser colada; caso contrario, <see langword="false"/>.</returns>
    /// <summary>
    /// Determina se uma peca pode ser colada no lado informado.
    /// </summary>
    public bool PodeColar(Peca peca, LadoTabuleiro lado)
    {
        // Se o tabuleiro está vazio, a primeira peça pode ser colada em qualquer lugar
        if (EstaVazio)
            return true;

        // Se escolheu a esquerda, verifica se a peça tem o número da PontaEsquerda
        // (Nota: se o seu Enum se chamar "Esquerdo" em vez de "Esquerda", ajuste aqui)
        if (lado == LadoTabuleiro.Esquerda)
            return peca.PossuiValor(PontaEsquerda!.Value);

        // Se escolheu a direita, verifica a PontaDireita
        return peca.PossuiValor(PontaDireita!.Value);
    }

    /// <summary>
    /// Cola uma peca no lado informado do tabuleiro.
    /// </summary>
    public void Colar(Peca peca, LadoTabuleiro lado)
    {
        if (!PodeColar(peca, lado))
            throw new InvalidOperationException("Jogada inválida: A peça não encaixa neste lado.");

        if (EstaVazio)
        {
            // A primeira peça simplesmente entra na mesa
            Pecas.Add(peca);
            return;
        }

        if (lado == LadoTabuleiro.Esquerda)
        {
            // Para colar na esquerda, o Lado Direito da nova peça (ValorB) deve tocar a PontaEsquerda atual.
            // Se o ValorB não for igual, significa que o número compatível está no ValorA, então invertemos.
            if (peca.ValorB != PontaEsquerda!.Value)
            {
                peca = peca.Inverter();
            }

            // Adiciona no início da lista (índice 0) para virar a nova PontaEsquerda
            Pecas.Insert(0, peca);
        }
        else
        {
            // Para colar na direita, o Lado Esquerdo da nova peça (ValorA) deve tocar a PontaDireita atual.
            // Se o ValorA não for igual, invertemos.
            if (peca.ValorA != PontaDireita!.Value)
            {
                peca = peca.Inverter();
            }

            // Adiciona no final da lista para virar a nova PontaDireita
            Pecas.Add(peca);
        }
    }

    /// <summary>
    /// Soma os valores das pontas externas atualmente expostas.
    /// Essa soma e a base para regras de pontuacao em que a rodada concede pontos quando o resultado for multiplo de 5.
    /// </summary>
    /// <returns>A soma das pontas externas, ou 0 quando o tabuleiro estiver vazio.</returns>
    public int SomarPontasExternas() =>
        EstaVazio ? 0 : PontaEsquerda!.Value + PontaDireita!.Value;

    /// <summary>
    /// Determina se o tabuleiro esta travado.
    /// O travamento e esperado quando nenhuma mao de jogador possuir peca compativel com as pontas externas atuais.
    /// </summary>
    /// <param name="maosJogadores">As maos dos jogadores da rodada.</param>
    /// <returns><see langword="true"/> quando o tabuleiro estiver travado; caso contrario, <see langword="false"/>.</returns>
    /// <summary>
    /// Determina se o tabuleiro esta travado.
    /// O travamento e esperado quando nenhuma mao de jogador possuir peca compativel com as pontas externas atuais.
    /// </summary>
    /// <param name="maosJogadores">As maos dos jogadores da rodada.</param>
    /// <returns><see langword="true"/> quando o tabuleiro estiver travado; caso contrario, <see langword="false"/>.</returns>
    public bool EstaTravado(IEnumerable<MaoJogador> maosJogadores)
    {
        // 1. Se não há peças no tabuleiro, ninguém jogou ainda, então não está travado
        if (!Pecas.Any())
            return false;

        // 2. Descobrir quais são os valores das pontas abertas no tabuleiro neste momento
        // O '!' (null-forgiving) garante ao compilador que já verificamos que não é nulo
        int valorPontaEsquerda = PontaEsquerda!.Value;
        int valorPontaDireita = PontaDireita!.Value;

        // 3. Verifica se algum jogador tem uma peça que sirva em alguma das pontas
        foreach (var mao in maosJogadores)
        {
            foreach (var peca in mao.Pecas)
            {
                // Usa o método da sua própria struct Peca!
                if (peca.PossuiValor(valorPontaEsquerda) || peca.PossuiValor(valorPontaDireita))
                {
                    // Alguém tem uma peça que encaixa, logo o jogo NÃO está travado
                    return false;
                }
            }
        }



        // 4. Varreu todas as mãos e ninguém tem peça que sirva
        return true;
    }

    /// <summary>
    /// Limpa o tabuleiro para preparar uma nova rodada.
    /// </summary>
    public void Limpar() =>
        Pecas.Clear();
}