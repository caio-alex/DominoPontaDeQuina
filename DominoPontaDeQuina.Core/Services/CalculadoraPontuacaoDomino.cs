namespace DominoPontaDeQuina.Core.Services;

public class CalculadoraPontuacaoDomino
{
    public int CalcularPontos(int somaDasPontas)
    {
        if (somaDasPontas < 0)
            throw new ArgumentOutOfRangeException(nameof(somaDasPontas), "A soma não pode ser negativa.");

        if (somaDasPontas == 0 || somaDasPontas % 5 != 0)
            return 0;

        return somaDasPontas / 5;
    }
}
