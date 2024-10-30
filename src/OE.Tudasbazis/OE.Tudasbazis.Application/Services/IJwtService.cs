using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Application.Services
{
	public interface IJwtService
	{
		/// <summary>
		///		Generates a JSON Web Token for the given user.
		/// </summary>
		/// <param name="userData">User data for login.</param>
		/// <returns>JSON Web Token</returns>
		public string GenerateToken(LoggedInUserDto userData);
	}
}
