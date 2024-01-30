using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordBinderCore.Repositories;
using WordBinderCore.Services;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((hostContext, services) =>
{
    services.AddSingleton<IWordRepository>(serviceProvider =>
    new WordRepository("input.txt"));
    services.AddTransient<IWordCombinationService, WordCombinationService>();
    services.AddTransient<AppRunner>();
});

var host = builder.Build();

using var serviceScope = host.Services.CreateScope();
var services = serviceScope.ServiceProvider;

try
{
    var app = services.GetRequiredService<AppRunner>();
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

Console.ReadLine();