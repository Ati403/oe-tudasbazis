using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OE.Tudasbazis.Application.DTOs;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public AuthController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		[HttpPost("register")]
		public IActionResult Register([FromBody] UserCreationDto userCreationDto)
		{
			string token = _tokenService.GenerateToken(userCreationDto);

			return Ok(token);
		}

		[HttpPost("asd")]
		[Authorize]
		public IActionResult Asd()
		{
			return Ok("asd");
		}
	}
}
