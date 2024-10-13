using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.DTOs.Requests
{
	public record LoginOrRegisterRequestDto
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
