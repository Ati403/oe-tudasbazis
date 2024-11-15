using System.Text.Json.Serialization;

namespace OE.Tudasbazis.Application.DTOs.Service
{
	public class EmbeddingRequestDto
	{
		[JsonPropertyName("text")]
		public string Text { get; set; } = string.Empty;
	}
}
