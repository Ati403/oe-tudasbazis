using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.Settings
{
	public class ConnectionStrings
	{
		public const string Section = "ConnectionStrings";

		[Required]
		public string DefaultConnection { get; set; } = string.Empty;
	}
}
