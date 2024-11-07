using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.Settings
{
	public class OpenAiSettings
	{
		public const string Section = "OpenAi";

		[Required]
		public string DeploymentId { get; set; } = string.Empty;

		[Required]
		public string ApiKey { get; set; } = string.Empty;

		[Required]
		public string Endpoint { get; set; } = string.Empty;
	}
}
