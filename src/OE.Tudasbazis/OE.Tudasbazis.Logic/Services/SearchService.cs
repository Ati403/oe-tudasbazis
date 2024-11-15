using OE.Tudasbazis.Application.DTOs.Responses;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.DataAccess;
using OE.Tudasbazis.Domain.Entities;

namespace OE.Tudasbazis.Logic.Services
{
	public class SearchService : ISearchService
	{
		private readonly IEmbeddingService _embeddingService;
		private readonly IElasticService _elasticService;
		private readonly IOpenAiService _openAiService;

		private readonly AppDbContext _dbContext;

		public SearchService(IEmbeddingService embeddingService, IElasticService elasticService, IOpenAiService openAiService, AppDbContext dbContext)
		{
			_embeddingService = embeddingService;
			_elasticService = elasticService;
			_openAiService = openAiService;

			_dbContext = dbContext;
		}

		public async Task<SearchResultDto> GetAnswerAsync(string question, Guid? userId)
		{
			float[] questionEmbedding = await _embeddingService.GetEmbeddingsAsync(question);

			var elasticDocuments = await _elasticService.SearchAsync(questionEmbedding);

			string answerContext = string.Join(" ", elasticDocuments.Select(document => document.Text));
			string answer = await _openAiService.GetAnswerAsync(question, answerContext);

			await SaveQuestionAnswerToLog(question, answer, userId);

			return new SearchResultDto
			{
				Answer = answer,
			};
		}

		private async Task SaveQuestionAnswerToLog(string question, string answer, Guid? userId)
		{
			_dbContext.QuestionAnswerLogs.Add(new QuestionAnswerLog
			{
				Question = question,
				Answer = answer,
				UserId = userId,
			});
			await _dbContext.SaveChangesAsync();
		}
	}
}
