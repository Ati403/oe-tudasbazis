using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Exceptions;

namespace OE.Tudasbazis.Application.Services
{
	public interface IAuthService
	{
		/// <summary>
		///		Register a new user.
		/// </summary>
		/// <param name="userCreationDto">User data for register a new user.</param>
		/// <exception cref="BusinessLogicException">Thrown when a user with the given username already exists.</exception>
		Task RegisterUserAsync(LoginOrRegisterRequestDto userCreationDto);

		/// <summary>
		///		Authenticate a user by logging them in.
		/// </summary>
		/// <param name="loginRequestDto">User data for login.</param>
		/// <returns>The authentication token.</returns>
		Task<JwtDto> LoginUserAsync(LoginOrRegisterRequestDto loginRequestDto);
	}
}
