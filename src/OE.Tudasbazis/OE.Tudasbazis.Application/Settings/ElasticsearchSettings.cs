using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.Settings
{
	public record ElasticsearchSettings
	{
		public const string Section = "Elasticsearch";

		[Required(AllowEmptyStrings = false)]
		public string Url { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		public string IndexName { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		public string ApiKey { get; set; } = string.Empty;
	}
}
