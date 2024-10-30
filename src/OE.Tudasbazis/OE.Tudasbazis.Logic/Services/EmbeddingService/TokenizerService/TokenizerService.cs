using FastBertTokenizer;

using OE.Tudasbazis.Application.DTOs.Service;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService.TokenizerService
{
	public abstract class TokenizerService
	{
		protected readonly BertTokenizer _tokenizer;

		public TokenizerService()
		{
			_tokenizer = new BertTokenizer();
		}

		public MLInputData GetTokenizedData(string text)
		{
			var (inputIds, attentionMask, tokenTypeIds) = _tokenizer.Encode(text);

			return new MLInputData
			{
				InputIds = inputIds.ToArray(),
				AttentionMask = attentionMask.ToArray()
			};
		}
	}
}
