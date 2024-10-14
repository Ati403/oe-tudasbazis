using OE.Tudasbazis.Application.Enums;

namespace OE.Tudasbazis.Application.DTOs.Responses
{
	public record LoggedInUserDto
	{
		public Guid Id { get; set; }

		public string Username { get; set; } = string.Empty;

		public UserRole Role { get; set; }
	}
}
