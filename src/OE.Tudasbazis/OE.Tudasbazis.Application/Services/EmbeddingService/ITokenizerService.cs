using OE.Tudasbazis.Application.DTOs.Service;

namespace OE.Tudasbazis.Application.Services.EmbeddingService
{
	public interface ITokenizerService
	{
		/// <summary>
		///		Generates tokenized data from the input string.
		/// </summary>
		/// <param name="text">Text to get embedded.</param>
		/// <returns>Input metadata for sentence transformer model.</returns>
		public MLInputData GetTokenizedData(string text);
	}
}
