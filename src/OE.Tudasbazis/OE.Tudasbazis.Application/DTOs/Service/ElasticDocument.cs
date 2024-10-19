namespace OE.Tudasbazis.Application.DTOs.Service
{
	public record ElasticDocument
	{
		public string Text { get; set; } = string.Empty;

		public float[] Vector { get; set; } = [];
	}
}
