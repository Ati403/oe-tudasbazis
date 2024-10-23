using FastBertTokenizer;

using OE.Tudasbazis.Application.Services.EmbeddingService;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService.TokenizerService
{
	public class OnlineTokenizerService : TokenizerService, ITokenizerService
	{
		private const string EMBEDDING_MODEL = "danieleff/hubert-base-cc-sentence-transformer";

		public OnlineTokenizerService()
		{
			_tokenizer.LoadFromHuggingFaceAsync(EMBEDDING_MODEL).Wait();
		}
	}
}
