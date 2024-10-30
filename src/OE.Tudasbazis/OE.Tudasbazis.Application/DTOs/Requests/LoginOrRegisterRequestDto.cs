using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.DTOs.Requests
{
	public record LoginOrRegisterRequestDto
	{
		[Required, MaxLength(120, ErrorMessage = "Username can't be longer than {1} characters.")]
		public string Username { get; set; } = string.Empty;

		[Required, Length(8, 120, ErrorMessage = "Password's length must be between {1} and {2} characters.")]
		public string Password { get; set; } = string.Empty;
	}
}
