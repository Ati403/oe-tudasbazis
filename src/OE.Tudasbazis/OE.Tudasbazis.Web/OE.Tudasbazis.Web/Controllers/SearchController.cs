using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;

using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SearchController : ControllerBase
	{
		private readonly ISearchService _searchService;

		public SearchController(ISearchService searchService)
		{
			_searchService = searchService;
		}

		/// <summary>
		///		Retrieves an answer for the given question.
		/// </summary>
		/// <param name="answerRequestDto">Contains the question to be answered.</param>
		/// <returns>The answer for the received question.</returns>
		[HttpPost]
		[Route("answer")]
		public async Task<IActionResult> GetAnswer([FromBody] AnswerRequestDto answerRequestDto)
		{
			bool isUserLoggedIn = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);

			var result = await _searchService.GetAnswerAsync(answerRequestDto.Question, isUserLoggedIn ? userId : null);

			return Ok(result);
		}
	}
}
