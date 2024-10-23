using Microsoft.ML.Data;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService
{
	public class BertInput
	{
		[VectorType(1, 7)]
		[ColumnName("input_ids")]
		public long[] InputIds { get; set; }

		[VectorType(1, 7)]
		[ColumnName("attention_mask")]
		public long[] AttentionMask { get; set; }

		[VectorType(1, 768)]
		[ColumnName("token_type_ids")]
		public long[] TokenTypeIds { get; set; }
	}
}
