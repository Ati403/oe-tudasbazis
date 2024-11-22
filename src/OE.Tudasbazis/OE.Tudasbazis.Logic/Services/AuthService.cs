using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Logic.Services
{
	/// <inheritdoc cref="IAuthService"/>
	public class AuthService : IAuthService
	{
		private readonly IUserService _userService;
		private readonly IJwtService _tokenService;

		public AuthService(IUserService userService, IJwtService tokenService)
		{
			_userService = userService;
			_tokenService = tokenService;
		}

		public async Task RegisterUserAsync(LoginOrRegisterRequestDto userCreationDto)
		{
			bool isUserExists = await _userService.CheckUserExists(userCreationDto);

			if (isUserExists)
			{
				throw new BusinessLogicException("A megadott felhasználónévvel már létezik felhasználó.");
			}

			await _userService.RegisterUserAsync(userCreationDto);
		}

		public async Task<JwtDto> LoginUserAsync(LoginOrRegisterRequestDto loginRequestDto)
		{
			var loggedInUser = await _userService.AuthenticateUserAsync(loginRequestDto);

			string token = _tokenService.GenerateToken(loggedInUser);

			var jwtDto = new JwtDto
			{
				Token = token,
			};

			return jwtDto;
		}
	}
}
