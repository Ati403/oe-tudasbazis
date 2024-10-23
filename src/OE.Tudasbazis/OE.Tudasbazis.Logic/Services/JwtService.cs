using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Settings;

namespace OE.Tudasbazis.Logic.Services
{
	public class JwtService: IJwtService
	{
		private readonly JwtSettings _jwtSettings;

		public JwtService(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		public string GenerateToken(LoggedInUserDto userData)
		{
			var claims = new Claim[]
			{
				new (ClaimTypes.NameIdentifier, userData.Id.ToString()),
				new (ClaimTypes.Name, userData.Username),
				new (ClaimTypes.Role, userData.Role.ToString())
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience,
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
