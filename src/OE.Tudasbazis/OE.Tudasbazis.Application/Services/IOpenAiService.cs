using OE.Tudasbazis.Application.Exceptions;

namespace OE.Tudasbazis.Application.Services
{
	public interface IOpenAiService
	{
		/// <summary>
		///		Generates answer for the given question from the given context with an AI LLM model.
		/// </summary>
		/// <param name="question">Question received from the user.</param>
		/// <param name="answerContext">Context received from the vector database KNN search result.</param>
		/// <exception cref="BusinessLogicException">Throws a Http 502 status code when an error occurs during the azure open ai request.</exception>
		/// <returns>Generated answer.</returns>
		Task<string> GetAnswerAsync(string question, string answerContext);
	}
}
