using Microsoft.ML.Data;

namespace OE.Tudasbazis.Application.DTOs.Service
{
	public class MLOutputData
	{
		[ColumnName("last_hidden_state")]
		[VectorType(768)]
		public float[] LastHiddenState { get; set; }
	}
}
