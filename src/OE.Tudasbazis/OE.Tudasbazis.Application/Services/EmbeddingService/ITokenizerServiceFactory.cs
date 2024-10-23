namespace OE.Tudasbazis.Application.Services.EmbeddingService
{
	public interface ITokenizerServiceFactory
	{
		/// <summary>
		///		Returns a TokenService based on the <paramref name="useOnlineVocab"/> flag.<para/>
		///		If <paramref name="useOnlineVocab"/> is true, the ML model vocabulary will be loaded from online.<para/>
		///		If <paramref name="useOnlineVocab"/> is false, the ML model vocabulary will be loaded from the local vocab.
		/// </summary>
		/// <param name="useOnlineVocab">Flag indicating whether to load the vocabulary from online or local.</param>
		/// <returns>An <see cref="ITokenizerService"/> instance.</returns>
		public ITokenizerService GetService(bool useOnlineVocab);
	}
}
