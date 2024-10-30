using OE.Tudasbazis.Application.Services.EmbeddingService;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService.TokenizerService
{
	public class OfflineTokenizerService : TokenizerService, ITokenizerService
	{
		private const string EMBEDDING_MODEL_VOCAB = "vocab.txt";

		public OfflineTokenizerService()
		{
			_tokenizer.LoadVocabulary(new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, EMBEDDING_MODEL_VOCAB)), convertInputToLowercase: true);
		}
	}
}
