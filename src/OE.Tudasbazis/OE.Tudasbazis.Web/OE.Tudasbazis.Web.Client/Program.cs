using Blazored.LocalStorage;
using Blazored.Toast;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.Services.AddBlazorBootstrap();
		builder.Services.AddBlazoredToast();
		builder.Services.AddBlazoredLocalStorage();

		builder.Services.AddAuthorizationCore();

		builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

		builder.Services.AddScoped(sp => new HttpClient
		{
			BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
		});

		await builder.Build().RunAsync();
	}
}