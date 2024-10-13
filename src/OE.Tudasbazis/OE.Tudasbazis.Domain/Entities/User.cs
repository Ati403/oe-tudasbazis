using OE.Tudasbazis.Application.Enums;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OE.Tudasbazis.Domain.Entities
{
	[Table("Users")]
	public class User
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MaxLength(120)]
		public string Username { get; set; } = string.Empty;

		[Required, Length(8, 120)]
		public string Password { get; set; } = string.Empty;

		[Required]
		public UserRole Role { get; set; } = UserRole.User;
	}
}
