using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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

			//Add Authentication and authorization
			var jwtSettings = configuration
				.GetSection(JwtSettings.Section)
				.Get<JwtSettings>();
			Validator.ValidateObject(jwtSettings!, new ValidationContext(jwtSettings!), validateAllProperties: true);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = jwtSettings!.Issuer,
					ValidAudience = jwtSettings!.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
					ClockSkew = TimeSpan.Zero,
				};
			});
			services.AddAuthorization();

			return services;
		}

		public static IServiceCollection BindSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new() { Title = "OE.Tudasbazis.Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement()
			  {
				{
				  new OpenApiSecurityScheme
				  {
					Reference = new OpenApiReference
					  {
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					  },
					  Scheme = "oauth2",
					  Name = "Bearer",
					  In = ParameterLocation.Header,

					},
					new List<string>()
				  }
				});

				string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			return services;
		}

		public static IServiceCollection BindOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddOptions<JwtSettings>().Bind(configuration.GetSection(JwtSettings.Section));

			return services;
		}
	}
}
