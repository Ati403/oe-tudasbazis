using OE.Tudasbazis.Application.Services.EmbeddingService;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService.TokenizerService
{
	public class TokenizerServiceFactory : ITokenizerServiceFactory
	{
		public ITokenizerService GetService(bool useOnlineVocab)
		{
			return useOnlineVocab
				? new OnlineTokenizerService()
				: new OfflineTokenizerService();
		}
	}
}
