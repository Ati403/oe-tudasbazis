using Microsoft.ML.Data;

namespace OE.Tudasbazis.Application.DTOs.Service
{
	public class MLInputData
	{
		[ColumnName("input_ids")]
		[VectorType(512)]
		public long[] InputIds { get; set; } = [];

		[ColumnName("attention_mask")]
		[VectorType(512)]
		public long[] AttentionMask { get; set; } = [];
	}
}
