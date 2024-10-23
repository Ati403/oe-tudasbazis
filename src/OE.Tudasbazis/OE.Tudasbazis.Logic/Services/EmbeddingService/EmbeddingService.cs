using FastBertTokenizer;

using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Services;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService
{
	public class EmbeddingService : IEmbeddingService
	{
		private readonly InferenceSession _session;

		public EmbeddingService(string modelPath)
		{
			_session = new InferenceSession("D:\\Dev\\Git\\oe-tudasbazis\\src\\OE.Tudasbazis\\OE.Tudasbazis.Web\\OE.Tudasbazis.Web\\bin\\Debug\\net8.0\\sentence_transformer.onnx");
		}

		public float[] GetEmbeddings(string text)
		{
			var mlInput = GenerateTokens(text).Result;

			var inputTensor1 = new DenseTensor<long>(mlInput.InputIds, [1, mlInput.InputIds.Length]);
			var inputTensor2 = new DenseTensor<long>(mlInput.AttentionMask, [1, mlInput.AttentionMask.Length]);
			var input = new NamedOnnxValue[]
			{
				NamedOnnxValue.CreateFromTensor("input_ids", inputTensor1),
				NamedOnnxValue.CreateFromTensor("attention_mask", inputTensor2),
			};

			using (var results = _session.Run(input))
			{
				var outputTensor = results.First().AsTensor<float>();

				int batchSize = outputTensor.Dimensions[0];
				int maxSeqLen = outputTensor.Dimensions[1];
				int embeddingSize = outputTensor.Dimensions[2];

				var pooledEmbedding = new float[embeddingSize];
				for (int i = 0; i < maxSeqLen; i++)  // Iterate over tokens
				{
					for (int j = 0; j < embeddingSize; j++)  // Iterate over embedding dimensions
					{
						pooledEmbedding[j] += outputTensor[0, i, j];
					}
				}

				// Átlagolás
				for (int j = 0; j < embeddingSize; j++)
				{
					pooledEmbedding[j] /= maxSeqLen;  // Average the embeddings
				}

				return pooledEmbedding;
			}
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
