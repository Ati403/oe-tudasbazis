using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Application.Services
{
	public interface ITokenService
	{
		/// <summary>
		///		Generates a token for the given user.
		/// </summary>
		/// <param name="userData">User data for login.</param>
		/// <returns>JWT token</returns>
		public string GenerateToken(LoggedInUserDto userData);
	}
}
