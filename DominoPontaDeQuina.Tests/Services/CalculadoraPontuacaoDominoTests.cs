using DominoPontaDeQuina.Core.Services;
using FluentAssertions;
using Xunit;

namespace DominoPontaDeQuina.Tests.Services;

public class CalculadoraPontuacaoDominoTests
{
    private readonly CalculadoraPontuacaoDomino _sut;

    public CalculadoraPontuacaoDominoTests()
    {
        _sut = new CalculadoraPontuacaoDomino();
    }

    [Theory]
    [InlineData(5, 1)]
    [InlineData(10, 2)]
    [InlineData(15, 3)]
    [InlineData(20, 4)]
    public void CalcularPontos_SomaMultiplaDe5_RetornaSomaDivididaPor5(int soma, int pontosEsperados)
    {
        // Act
        var resultado = _sut.CalcularPontos(soma);

        // Assert
        resultado.Should().Be(pontosEsperados);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(12)]
    public void CalcularPontos_SomaNaoMultiplaDe5_RetornaZero(int somaInvalida)
    {
        // Act
        var resultado = _sut.CalcularPontos(somaInvalida);

        // Assert
        resultado.Should().Be(0);
    }

    [Fact]
    public void CalcularPontos_SomaNegativa_LancaExcecao()
    {
        // Arrange
        int somaInvalida = -5;

        // Act
        Action act = () => _sut.CalcularPontos(somaInvalida);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>()
           .WithMessage("*não pode ser negativa*");
    }
}