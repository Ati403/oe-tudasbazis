using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OE.Tudasbazis.Application.DTOs;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Settings;

namespace OE.Tudasbazis.Logic.Services
{
	public class TokenService : ITokenService
	{
		private readonly JwtSettings _jwtSettings;

		public TokenService(IOptions<JwtSettings> jwtSettings)
		{
			_jwtSettings = jwtSettings.Value;
		}

		public string GenerateToken(UserCreationDto userCreationDto)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new (ClaimTypes.Name, userCreationDto.Username),
				}),
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
