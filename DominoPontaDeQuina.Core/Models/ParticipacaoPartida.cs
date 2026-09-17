namespace DominoPontaDeQuina.Core.Models
{
    public class ParticipacaoPartida
    {
        public Guid Id { get; set; }
        public int Pontos { get; set; }
        public virtual Partida? Partida { get; set; }
        public virtual Jogador? Jogador { get; set; }
    }
}