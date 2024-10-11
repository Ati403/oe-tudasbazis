using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.Tudasbazis.Application.DTOs
{
	public record UserCreationDto
	{
		[Required]
		public string Username { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
