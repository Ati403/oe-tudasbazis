using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Domain.Entities
{
	public class QuestionAnswerLog
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(2000)]
		public string Question { get; set; } = string.Empty;

		[Required, MaxLength(2000)]
		public string Answer { get; set; } = string.Empty;
	}
}
