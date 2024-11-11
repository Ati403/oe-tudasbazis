using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Application.Services
{
	public interface ISearchService
	{
		/// <summary>
		///		Provide an answer for the given question.
		/// </summary>
		/// <param name="question">Question received from the user.</param>
		/// <param name="userId">Contains the user's identifier if they are logged in.</param>
		/// <returns>Answer for the given <paramref name="question"/>.</returns>
		Task<SearchResultDto> GetAnswerAsync(string question, Guid? userId);
	}
}
