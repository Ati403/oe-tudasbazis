using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OE.Tudasbazis.Application.Enums;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = nameof(UserRole.User))]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("history")]
		public async Task<IActionResult> GetQuestionAnswerHistoryAsync()
		{
			var userId = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

			var history = await _userService.GetQuestionAnswerHistoryAsync(userId);

			return Ok(history);
		}
	}
}
