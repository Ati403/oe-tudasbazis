using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OE.Tudasbazis.DataAccess
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			return services;
		}
	}
}
