using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OE.Tudasbazis.Logic
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddLogic(this IServiceCollection services, IConfiguration configuration)
		{
			return services;
		}
	}
}
