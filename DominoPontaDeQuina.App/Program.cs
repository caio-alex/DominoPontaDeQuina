using DominoPontaDeQuina.App;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Repository.Repositories;
using DominoPontaDeQuina.Service.Interfaces;
using DominoPontaDeQuina.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddDbContext<DominoDbContext>(options =>
    options.UseSqlite("Data Source=domino.db"));

services.AddScoped<IUsuarioRepository, UsuarioRepository>();
services.AddScoped<IUsuarioService, UsuarioService>();
services.AddScoped<MenuPrincipal>();

using var provider = services.BuildServiceProvider();
using var scope = provider.CreateScope();

var menu = scope.ServiceProvider.GetRequiredService<MenuPrincipal>();
await menu.ExecutarAsync();
