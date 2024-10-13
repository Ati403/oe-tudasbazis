using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using OE.Tudasbazis.Application.Settings;
using OE.Tudasbazis.Web;

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

					},
					new List<string>()
				  }
				});
			//var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			//var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			//c.IncludeXmlComments(xmlPath);
		});

		builder.Services.AddScoped(http => new HttpClient
		{
			BaseAddress = new Uri("Https://localhost:7165")
		});

		builder.Services.BindServices(builder.Configuration);

		builder.Services.AddOptions<JwtSettings>().Bind(builder.Configuration.GetSection(JwtSettings.Section));

		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
					ClockSkew = TimeSpan.Zero,
				};
			});
		builder.Services.AddAuthorization();

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
		app.UseAntiforgery();

		app.MapRazorComponents<OE.Tudasbazis.Web.Components.App>()
			.AddInteractiveWebAssemblyRenderMode()
			.AddAdditionalAssemblies(typeof(OE.Tudasbazis.Web.Client._Imports).Assembly);

		app.UseRouting();
		
		app.UseMiddleware<BusinessExceptionHandlingMiddleware>();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}