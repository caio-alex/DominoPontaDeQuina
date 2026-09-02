using DominoPontaDeQuina.Service.Interfaces;

namespace DominoPontaDeQuina.App;

public class MenuPrincipal
{
    private readonly IUsuarioService _usuarioService;

    public MenuPrincipal(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public async Task ExecutarAsync()
    {
        var opcao = string.Empty;

        while (opcao != "3")
        {
            Console.WriteLine();
            Console.WriteLine("=== Domino Ponta de Quina ===");
            Console.WriteLine("1. Cadastrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Sair");
            Console.Write("Escolha uma opcao: ");
            opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    await CadastrarUsuarioAsync();
                    break;
                case "2":
                    await ListarUsuariosAsync();
                    break;
                case "3":
                    Console.WriteLine("Ate logo!");
                    break;
                default:
                    Console.WriteLine("Opcao invalida.");
                    break;
            }
        }
    }

    private async Task CadastrarUsuarioAsync()
    {
        Console.Write("Nome: ");
        var nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Email: ");
        var email = Console.ReadLine() ?? string.Empty;

        Console.Write("Senha: ");
        var senha = Console.ReadLine() ?? string.Empty;

        try
        {
            var usuario = await _usuarioService.CadastrarAsync(nome, email, senha);
            Console.WriteLine($"Usuario '{usuario.Nome}' cadastrado com id {usuario.Id}.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Nao foi possivel cadastrar: {ex.Message}");
        }
    }

    private async Task ListarUsuariosAsync()
    {
        var usuarios = await _usuarioService.ListarAsync();

        if (usuarios.Count == 0)
        {
            Console.WriteLine("Nenhum usuario cadastrado ainda.");
            return;
        }

        foreach (var usuario in usuarios)
        {
            Console.WriteLine($"- {usuario.Nome} ({usuario.Email})");
        }
    }
}
