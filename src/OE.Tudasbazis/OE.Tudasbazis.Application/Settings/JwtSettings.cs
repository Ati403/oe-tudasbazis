using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.Settings
{
	public class JwtSettings
	{
		public const string Section = "Jwt";

		[Required]
		public string Key { get; set; } = string.Empty;

		[Required]
		public string Issuer { get; set; } = string.Empty;

		[Required]
		public string Audience { get; set; } = string.Empty;
	}
}
