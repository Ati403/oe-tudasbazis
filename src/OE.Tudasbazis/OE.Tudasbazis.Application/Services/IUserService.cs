using OE.Tudasbazis.Application.DTOs.Requests;
using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Application.Services
{
    public interface IUserService
    {
        /// <summary>
        ///		Check if a user already exists with the given username.
        /// </summary>
        /// <param name="userCreationDto">User data for registering a new user.</param>
        /// <returns>True if a user already exists with the given username, false otherwise.</returns>
        Task<bool> CheckUserExists(LoginOrRegisterRequestDto userCreationDto);

        /// <summary>
        ///		Register a new user.
        /// </summary>
        /// <param name="userCreationDto">User data for registering a new user.</param>
        Task RegisterUserAsync(LoginOrRegisterRequestDto userCreationDto);

		/// <summary>
		///		Get a user by their username.
		/// </summary>
		/// <param name="loginDto">User data for authenticating a user.</param>
		/// <exception cref="BusinessLogicException">Thrown when the user does not exist or the password is incorrect.</exception>
		/// <returns>The logged-in user information.</returns>
		Task<LoggedInUserDto> AuthenticateUserAsync(LoginOrRegisterRequestDto loginDto);
    }
}
