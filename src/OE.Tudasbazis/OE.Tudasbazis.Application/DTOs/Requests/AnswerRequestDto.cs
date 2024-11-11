using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.DTOs.Requests
{
	public record AnswerRequestDto
	{
		[Required]
		public string Question { get; set; } = string.Empty;
	}
}
