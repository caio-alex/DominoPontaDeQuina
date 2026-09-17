namespace DominoPontaDeQuina.Core.Models
{
	public class Lance
	{
		public Guid Id { get; set; }
		public DateTime Timestamp { get; set; }
		public virtual Partida? Partida { get; set; }
		public virtual Jogador? Jogador { get; set; }
	}
}