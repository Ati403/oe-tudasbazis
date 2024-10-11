using OE.Tudasbazis.Application.DTOs;

namespace OE.Tudasbazis.Application.Services
{
	public interface ITokenService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="userCreationDto"></param>
		/// <returns></returns>
		public string GenerateToken(UserCreationDto userCreationDto);
	}
}
