namespace OE.Tudasbazis.Application.DTOs.Responses
{
	public record QuestionAnswerHistoryDto
	{
		public string Question { get; set; } = string.Empty;

		public string Answer { get; set; } = string.Empty;

		public DateTime Timestamp { get; set; } = new DateTime();
	}
}
