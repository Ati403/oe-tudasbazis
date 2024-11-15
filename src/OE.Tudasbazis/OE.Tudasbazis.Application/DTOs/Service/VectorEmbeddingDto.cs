using System.Text.Json.Serialization;

namespace OE.Tudasbazis.Application.DTOs.Service
{
	public class VectorEmbeddingDto
	{
		[JsonPropertyName("vector_embedding")]
		public float[] Embedding { get; set; } = [];
	}
}
