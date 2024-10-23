using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Onnx;

using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Services;

using FastBertTokenizer;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService
{
	public class EmbeddingService : IEmbeddingService
	{
		private readonly MLContext _mlContext;
		private readonly ITransformer _onnxModel;
		private readonly PredictionEngine<MLInputData, MLOutputData> _predictionEngine;
		//private readonly PredictionEngine<BertInput, MLOutputData> _predictionEngine;

		public EmbeddingService(string modelPath)
		{
			//_mlContext = new MLContext();

			//var pipeline = _mlContext.Transforms.ApplyOnnxModel(
			//	modelFile: modelPath,
			//	inputColumnNames: ["input_ids", "attention_mask"],
			//	outputColumnNames: ["last_hidden_state"]);

			//var emptyData = _mlContext.Data.LoadFromEnumerable(new List<MLInputData>());
			//_onnxModel = pipeline.Fit(emptyData);
			//_predictionEngine = _mlContext.Model.CreatePredictionEngine<MLInputData, MLOutputData>(_onnxModel);
		}

		public float[] GetEmbeddings(string text)
		{
			var mlInput = GenerateTokens(text).Result;

			//var result = _predictionEngine.Predict(mlInput);
			using var runOptions = new RunOptions();
			using var session = new InferenceSession("D:\\Dev\\Git\\oe-tudasbazis\\src\\OE.Tudasbazis\\OE.Tudasbazis.Web\\OE.Tudasbazis.Web\\bin\\Debug\\net8.0\\sentence_transformer.onnx");

			using var inputIdsOrtValue = OrtValue.CreateTensorValueFromMemory(mlInput.InputIds, [1, mlInput.InputIds.Length]);
			using var attMaskOrtValue = OrtValue.CreateTensorValueFromMemory(mlInput.AttentionMask, [1, mlInput.AttentionMask.Length]);

			var inputs = new Dictionary<string, OrtValue>
			{
				{ "input_ids", inputIdsOrtValue },
				{ "attention_mask", attMaskOrtValue }
			};

			var output = session.Run(runOptions, inputs, session.OutputNames);
			var fullOutput = output.ToArray();
			float[] embeddings = output[output.Count - 1].GetTensorDataAsSpan<float>().ToArray();

			return Array.Empty<float>();
		}

		private int GetMaxValueIndex(ReadOnlySpan<float> span)
		{
			float maxVal = span[0];
			int maxIndex = 0;
			for (int i = 1; i < span.Length; ++i)
			{
				float v = span[i];
				if (v > maxVal)
				{
					maxVal = v;
					maxIndex = i;
				}
			}
			return maxIndex;
		}

		private async Task<MLInputData> GenerateTokens(string text)
		{
			var tokenizer = new BertTokenizer();
			await tokenizer.LoadFromHuggingFaceAsync("danieleff/hubert-base-cc-sentence-transformer");
			var (inputIds, attentionMask, tokenTypeIds) = tokenizer.Encode(text);

			var inputIdsArray = inputIds.ToArray();

			return new MLInputData
			{
				InputIds = inputIds.ToArray(),
				AttentionMask = attentionMask.ToArray()
			};
		}
	}
}
