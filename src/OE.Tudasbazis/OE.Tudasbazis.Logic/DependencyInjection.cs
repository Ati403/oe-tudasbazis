using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Services.EmbeddingService;
using OE.Tudasbazis.Logic.Services;
using OE.Tudasbazis.Logic.Services.EmbeddingService;

namespace OE.Tudasbazis.Logic
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddLogic(this IServiceCollection services)
		{
			services.AddSingleton<IJwtService, JwtService>();
			services.AddScoped<IEmbeddingService, EmbeddingService>(sp => new EmbeddingService(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sentence_transformer.onnx")));

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IElasticService, ElasticService>();

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
