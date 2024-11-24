using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OE.Tudasbazis.Domain.Entities
{
	[Table("QuestionAnswerLogs")]
	public class QuestionAnswerLog
	{
		[Key]
		public Guid Id { get; set; }

		[Required, MaxLength(2000)]
		public string Question { get; set; } = string.Empty;

		[Required, MaxLength(2000)]
		public string Answer { get; set; } = string.Empty;

		public Guid? UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; } = default!;

		[Required]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
