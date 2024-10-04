using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using OE.Tudasbazis.Web.Client.Services;

internal class Program
{
	private static async global::System.Threading.Tasks.Task Main(string[] args)
	{
		var builder = WebAssemblyHostBuilder.CreateDefault(args);

		builder.Services.AddScoped(http => new HttpClient
		{
			BaseAddress = new Uri("Https://localhost:7165")
		});

		await builder.Build().RunAsync();
	}
}