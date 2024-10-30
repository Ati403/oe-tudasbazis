namespace OE.Tudasbazis.Application.Settings
{
	public record EmbeddingModelSettings
	{
		public const string Section = "EmbeddingModel";

		public bool UseOnlineVocabulary { get; set; }
	}
}
