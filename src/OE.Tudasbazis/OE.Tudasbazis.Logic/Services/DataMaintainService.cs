
using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Services.EmbeddingService;

namespace OE.Tudasbazis.Logic.Services
{
	public class DataMaintainService : IDataMaintainService
	{
		private readonly IEmbeddingService _embeddingService;
		private readonly IElasticService _elasticService;
		private readonly IPdfProcessorService _pdfProcessorService;

		public DataMaintainService(IEmbeddingService embeddingService, IElasticService elasticService, IPdfProcessorService pdfProcessorService)
		{
			_embeddingService = embeddingService;
			_elasticService = elasticService;
			_pdfProcessorService = pdfProcessorService;
		}

		public async Task UploadStringToVectorDatabaseAsync(string text)
		{
			float[] vectorEmbedding = _embeddingService.GetEmbeddings(text);


			//TODO: Create generic method for this
			var elasticDocument = new ElasticDocument
			{
				Text = text,
				Vector = vectorEmbedding,
			};

			await _elasticService.IndexEmbeddingAsync(elasticDocument);
		}

		public async Task UploadPdfToVectoDatabaseAsync(string fileName, Stream pdfStream)
		{
			ValidateFileExtension(fileName);

			var textsToUpload = _pdfProcessorService.ProcessPdf(pdfStream);

			var elasticDocuments = GenerateElasticDocuments(textsToUpload);

			foreach (var elasticDocument in elasticDocuments)
			{
				await _elasticService.IndexEmbeddingAsync(elasticDocument);
			}
		}

		private void ValidateFileExtension(string fileName)
		{
			string extension = Path.GetExtension(fileName).ToLowerInvariant();

			if (extension != ".pdf")
			{
				throw new BusinessLogicException("Csak PDF fájlt lehet feltölteni!") { StatusCode = 400 };
			}
		}

		private List<ElasticDocument> GenerateElasticDocuments(List<string> texts)
		{
			var embeddings = new List<ElasticDocument>();

			foreach (string text in texts)
			{
				embeddings.Add(new ElasticDocument
				{
					Text = text,
					Vector = _embeddingService.GetEmbeddings(text),
				});
			}

			return embeddings;
		}
	}
}
