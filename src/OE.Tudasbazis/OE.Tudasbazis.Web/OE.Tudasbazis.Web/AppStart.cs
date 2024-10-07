using System.ComponentModel.DataAnnotations;

using OE.Tudasbazis.Application.Settings;
using OE.Tudasbazis.DataAccess;
using OE.Tudasbazis.Logic;

namespace OE.Tudasbazis.Web
{
	public static class AppStart
	{
		public static IServiceCollection BindServices(this IServiceCollection services, IConfiguration configuration)
		{
			//Bind Data Access Layer
			var connectionStrings = configuration
				.GetSection(ConnectionStrings.Section)
				.Get<ConnectionStrings>();
			Validator.ValidateObject(connectionStrings!, new ValidationContext(connectionStrings!), validateAllProperties: true);
			services.AddDataAccess(connectionStrings!.DefaultConnection);

			//Bind Business Logic Layer
			services.AddLogic();

			return services;
		}
	}
}
