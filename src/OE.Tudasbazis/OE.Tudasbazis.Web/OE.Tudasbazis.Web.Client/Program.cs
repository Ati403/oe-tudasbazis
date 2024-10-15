using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Blazored.Toast;
using OE.Tudasbazis.Web.Client;
using Microsoft.AspNetCore.Components.Authorization;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.Services.AddBlazorBootstrap();

		builder.Services.AddScoped(http => new HttpClient
		builder.RootComponents.Add<App>("#app");
		builder.RootComponents.Add<HeadOutlet>("head::after");

		builder.Services.AddBlazoredToast(); //Toast notifications

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