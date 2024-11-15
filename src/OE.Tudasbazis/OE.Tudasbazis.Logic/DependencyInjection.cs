using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Logic.Services;

namespace OE.Tudasbazis.Logic
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddLogic(this IServiceCollection services)
		{
			services.AddSingleton<IJwtService, JwtService>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IElasticService, ElasticService>();
			services.AddScoped<IDataMaintainService, DataMaintainService>();
			services.AddScoped<ISearchService, SearchService>();

			services.AddTransient<IPdfProcessorService, PdfProcessorService>();
			services.AddTransient<IOpenAiService, OpenAiService>();
			services.AddTransient<IEmbeddingService, EmbeddingService>();

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
