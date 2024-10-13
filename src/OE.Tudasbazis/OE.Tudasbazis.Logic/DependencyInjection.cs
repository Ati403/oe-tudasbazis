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
			services.AddSingleton<ITokenService, TokenService>();

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
