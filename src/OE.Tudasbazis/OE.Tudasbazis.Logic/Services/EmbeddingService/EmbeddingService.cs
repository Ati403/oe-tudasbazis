using Microsoft.Extensions.Options;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

using OE.Tudasbazis.Application.Services.EmbeddingService;
using OE.Tudasbazis.Application.Settings;

namespace OE.Tudasbazis.Logic.Services.EmbeddingService
{
	public class EmbeddingService : IEmbeddingService
	{
		private readonly InferenceSession _session;
		private readonly ITokenizerService _tokenizerService;
		private readonly EmbeddingModelSettings _embeddingModelSettings;

		public EmbeddingService(IOptions<EmbeddingModelSettings> embeddingModelSettings, ITokenizerServiceFactory tokenizerServiceFactory)
		{
			string modelOnnxFile = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.onnx").First();
			_session = new InferenceSession(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, modelOnnxFile));

			_embeddingModelSettings = embeddingModelSettings.Value;

			_tokenizerService = tokenizerServiceFactory.GetService(_embeddingModelSettings.UseOnlineVocabulary);
		}

		public float[] GetEmbeddings(string text)
		{
			var tokenizedData = _tokenizerService.GetTokenizedData(text);

			var inputIdsTensor = new DenseTensor<long>(tokenizedData.InputIds, [1, tokenizedData.InputIds.Length]);
			var attentionMaskTensor = new DenseTensor<long>(tokenizedData.AttentionMask, [1, tokenizedData.AttentionMask.Length]);
			var input = new NamedOnnxValue[]
			{
				NamedOnnxValue.CreateFromTensor("input_ids", inputIdsTensor),
				NamedOnnxValue.CreateFromTensor("attention_mask", attentionMaskTensor),
			};

			using var results = _session.Run(input);
			var outputTensor = results[0].AsTensor<float>();

			float[] embedding = GetAveragePooling(outputTensor);

			return embedding;
		}

		private static float[] GetAveragePooling(Tensor<float> outputTensor)
		{
			int maxSeqLen = outputTensor.Dimensions[1];
			int embeddingSize = outputTensor.Dimensions[2];

			float[] pooledEmbedding = new float[embeddingSize];

			// Iterate over tokens
			for (int i = 0; i < maxSeqLen; i++)
			{
				// Iterate over embedding dimensions
				for (int j = 0; j < embeddingSize; j++)
				{
					pooledEmbedding[j] += outputTensor[0, i, j];
				}
			}

			// Average the embeddings
			for (int j = 0; j < embeddingSize; j++)
			{
				pooledEmbedding[j] /= maxSeqLen;
			}

			return pooledEmbedding;
		}
	}
}
