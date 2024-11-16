using OE.Tudasbazis.Web;
using OE.Tudasbazis.Web.Middlewares;
using OE.Tudasbazis.Web.Client.Components;

internal class Program
{
	private static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddRazorComponents()
			.AddInteractiveWebAssemblyComponents();
		builder.Services.AddEndpointsApiExplorer();

		//builder.Services.AddScoped(http => new HttpClient
		//{
		//	BaseAddress = new Uri("Https://localhost:7165"),
		//});

		builder.Services.AddHttpClient();

		builder.Services.BindSwagger();
		builder.Services.BindOptions(builder.Configuration);
		builder.Services.BindServices(builder.Configuration);

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseWebAssemblyDebugging();
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		else
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.MapRazorComponents<App>()
			.AddInteractiveWebAssemblyRenderMode();

		app.UseRouting();

		app.UseMiddleware<RateLimitingMiddleware>();
		app.UseMiddleware<BusinessExceptionHandlingMiddleware>();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAntiforgery();

		app.MapControllers();

		app.Run();
	}
}