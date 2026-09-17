namespace DominoPontaDeQuina.Core.Models
{
    public class Ranking
    {
        public Guid Id { get; set; }
        public string JogadorNome { get; set; }
        public int Vitorias { get; set; }

        public virtual Jogador? Jogador { get; set; }
    }
}