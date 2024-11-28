using System.ClientModel;
using System.Text.RegularExpressions;

using Azure.AI.OpenAI;

using Microsoft.Extensions.Options;

using OE.Tudasbazis.Application.Exceptions;
using OE.Tudasbazis.Application.Services;
using OE.Tudasbazis.Application.Settings;

using OpenAI.Chat;

namespace OE.Tudasbazis.Logic.Services
{
	public partial class OpenAiService : IOpenAiService
	{
		private const string SYSTEM_MESSAGE = "You are a member of the university's administration team, you will receive questions from the students and you have to answer hungarian from the given context. If the given context doesn't contain the relevant answer for the received text, the answer should be: Erre a kérdésre nem tudok válaszolni. The format of the answer should be, Answer: <answer>.";

		private readonly OpenAiSettings _openAiSettings;
		private readonly ChatClient _openAiChatClient;

		public OpenAiService(IOptions<OpenAiSettings> openAiSettings)
		{
			_openAiSettings = openAiSettings.Value;

			var openAiClient = new AzureOpenAIClient(
				new Uri(_openAiSettings.Endpoint),
				new ApiKeyCredential(_openAiSettings.ApiKey));
			_openAiChatClient = openAiClient.GetChatClient(_openAiSettings.DeploymentId);
		}

		public async Task<string> GetAnswerAsync(string question, string answerContext)
		{
			ClientResult<ChatCompletion>? completion;

			try
			{
				var chatOptions = new ChatCompletionOptions
				{
					Temperature = _openAiSettings.Temperature,
					MaxOutputTokenCount = 1000,
				};

				var messages = new List<ChatMessage>
				{
					new SystemChatMessage(SYSTEM_MESSAGE),
					new SystemChatMessage(answerContext),
					new UserChatMessage(question)
				};

				completion = await _openAiChatClient.CompleteChatAsync(messages, chatOptions);
			}
			catch (Exception)
			{
				throw new BusinessLogicException("Hiba a válaszgenerálása során, próbáld meg később.") { StatusCode = 502 };
			}

			var answer = completion.Value.Content.FirstOrDefault();
			if (answer is null)
			{
				return "Erre a kérdésre nem tudok válaszolni.";
			}
			else
			{
				string answerClear = Regex.Replace(answer.Text, "Answer:", string.Empty);

				return answerClear;
			}
		}
	}
}
