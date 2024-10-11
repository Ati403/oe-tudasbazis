using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Logic.Services;

namespace OE.Tudasbazis.Logic
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddLogic(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<ITokenService, TokenService>();

			return services;
		}
	}
}
