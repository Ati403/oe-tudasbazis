using System.Net.Http.Json;

using Microsoft.Extensions.Options;

using OE.Tudasbazis.Application.DTOs.Service;
using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Settings;

namespace OE.Tudasbazis.Logic.Services
{
	public class EmbeddingService : IEmbeddingService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly EmbeddingApiSettings _embeddingApiSettings;

		public EmbeddingService(IHttpClientFactory httpClientFactory, IOptions<EmbeddingApiSettings> embeddingApiSettings)
		{
			_httpClientFactory = httpClientFactory;
			_embeddingApiSettings = embeddingApiSettings.Value;
		}

		public async Task<float[]> GetEmbeddingsAsync(string text)
		{
			var httpClient = _httpClientFactory.CreateClient();
			var baseUrl = new Uri(_embeddingApiSettings.BaseUrl, UriKind.Absolute);
			var endpointUrl = new Uri(baseUrl, _embeddingApiSettings.EmbeddingEndpoint);

			var embdeddingRequest = new EmbeddingRequestDto
			{
				Text = text
			};

			var response = await httpClient.PostAsJsonAsync<EmbeddingRequestDto>(endpointUrl, embdeddingRequest);

			if (response.IsSuccessStatusCode)
			{
				var embeddingResponse = await response.Content.ReadFromJsonAsync<VectorEmbeddingDto>();

				if (embeddingResponse is not null)
				{
					return embeddingResponse.Embedding;
				}
			}

			throw new BusinessLogicException("Hiba történt a kérdés beágyazásakor.") { StatusCode = 502 };
		}
	}
}
