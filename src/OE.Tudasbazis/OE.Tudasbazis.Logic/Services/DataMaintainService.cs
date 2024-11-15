using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;

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
			var elasticDocument = await GenerateElasticDocuments([text]);

			await UploadDocumentToVectorDatabase(elasticDocument);
		}

		public async Task UploadPdfToVectoDatabaseAsync(string fileName, Stream pdfStream)
		{
			ValidateFileExtension(fileName);

			var textsToUpload = _pdfProcessorService.ProcessPdf(pdfStream);

			var elasticDocuments = await GenerateElasticDocuments(textsToUpload);

			await UploadDocumentToVectorDatabase(elasticDocuments);
		}

		private static void ValidateFileExtension(string fileName)
		{
			string extension = Path.GetExtension(fileName).ToLowerInvariant();

			if (extension != ".pdf")
			{
				throw new BusinessLogicException("Csak PDF fájlt lehet feltölteni!") { StatusCode = 400 };
			}
		}

		private async Task<List<ElasticDocument>> GenerateElasticDocuments(List<string> texts)
		{
			var elasticDocuments = new List<ElasticDocument>();

			foreach (string text in texts)
			{
				var elasticDocument = new ElasticDocument
				{
					Text = text,
					Vector = await _embeddingService.GetEmbeddingsAsync(text),
				};

				elasticDocuments.Add(elasticDocument);
			}

			return elasticDocuments;
		}

		private async Task UploadDocumentToVectorDatabase(IEnumerable<ElasticDocument> elasticDocuments)
		{
			foreach (var doc in elasticDocuments)
			{
				await _elasticService.IndexEmbeddingAsync(doc);
			}
		}
	}
}
