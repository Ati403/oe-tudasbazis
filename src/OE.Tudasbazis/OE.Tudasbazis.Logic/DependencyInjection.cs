using Microsoft.Extensions.DependencyInjection;

using OE.Tudasbazis.Logic.Services;

namespace OE.Tudasbazis.Logic
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddLogic(this IServiceCollection services)
		{
			services.AddScoped<TestService>();

			return services;
		}
	}
}
