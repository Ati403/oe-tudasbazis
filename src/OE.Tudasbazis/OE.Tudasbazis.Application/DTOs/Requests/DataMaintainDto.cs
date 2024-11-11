using System.ComponentModel.DataAnnotations;

namespace OE.Tudasbazis.Application.DTOs.Requests
{
	public class DataMaintainDto
	{
		[Required]
		public string Text { get; set; } = string.Empty;
	}
}
