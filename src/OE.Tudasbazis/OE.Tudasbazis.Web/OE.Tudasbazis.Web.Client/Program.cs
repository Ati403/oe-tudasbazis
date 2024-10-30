using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using OE.Tudasbazis.Web.Client.Services;

internal class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.Services.AddBlazorBootstrap();

		builder.Services.AddScoped(http => new HttpClient
		{
			BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
		});

		await builder.Build().RunAsync();
	}
}