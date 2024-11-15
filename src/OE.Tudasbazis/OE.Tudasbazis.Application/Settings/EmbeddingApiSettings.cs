using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.Settings
{
	public class EmbeddingApiSettings
	{
		public const string Section = "EmbeddingApi";

		[Required]
		public string BaseUrl { get; set; } = string.Empty;

		[Required]
		public string EmbeddingEndpoint { get; set; } = string.Empty;
	}
}
