using Elastic.Clients.Elasticsearch;
using Elastic.Transport;

using Microsoft.Extensions.Options;

using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Settings;

namespace OE.Tudasbazis.Logic.Services
{
	public class ElasticService : IElasticService
	{
		private readonly ElasticsearchClient _client;
		private readonly ElasticsearchSettings _settings;

		public ElasticService(IOptions<ElasticsearchSettings> settings)
		{
			_settings = settings.Value;

			var connectionSettings = new ElasticsearchClientSettings(new Uri(_settings.Url))
				.Authentication(new ApiKey(_settings.ApiKey))
				.DefaultIndex(_settings.IndexName);

			_client = new ElasticsearchClient(connectionSettings);
		}

		public async Task IndexEmbeddingAsync(ElasticDocument document)
		{
			var indexResponse = await _client.IndexAsync(document);

			if (!indexResponse.IsValidResponse)
			{
				throw new BusinessLogicException("Error during indexing new document") { StatusCode = 502 };
			}
		}

		public async Task<List<ElasticDocument>> SearchAsync(float[] queryEmbedding, int topK = 15)
		{
			var queryResponse = await _client.SearchAsync<ElasticDocument>(s => s
				.Query(q => q
					.Knn(k => k
						.Field(new Field(nameof(ElasticDocument.Vector).ToLower()))
						.QueryVector(queryEmbedding)
						.k(topK)
					)
				));

			if (!queryResponse.IsValidResponse)
			{
				throw new BusinessLogicException("Error during searching for similar documents") { StatusCode = 502 };
			}

			return queryResponse.Documents.ToList();
		}
	}
}
