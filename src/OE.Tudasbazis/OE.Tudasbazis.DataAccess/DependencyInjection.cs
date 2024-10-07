using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
