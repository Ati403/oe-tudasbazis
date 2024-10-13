using Microsoft.AspNetCore.Mvc;

using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		/// <summary>
		///		Register a new user.
		/// </summary>
		/// <param name="userCreationDto">User data for register a new user.</param>
		[HttpPost]
		public async Task<IActionResult> Register(
			[FromBody] LoginOrRegisterRequestDto userCreationDto)
		{
			await _authService.RegisterUserAsync(userCreationDto);

			return Ok(new { Message = "Registration successful" });
		}

		/// <summary>
		///		Authenticate a user by logging them in.
		/// </summary>
		/// <param name="loginRequestDto">User data for login.</param>
		/// <returns>The authentication token.</returns>
		[HttpPost]
		public async Task<ActionResult<JwtDto>> Login(
			[FromBody] LoginOrRegisterRequestDto loginRequestDto)
		{
			var jwt = await _authService.LoginUserAsync(loginRequestDto);

			return Ok(jwt);
		}
	}
}
