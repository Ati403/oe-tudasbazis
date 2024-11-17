using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;
using OE.Tudasbazis.Application.DTOs.Requests;

using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Web.Client.Pages
{
	public partial class Home : ComponentBase
	{
		private string UserInput { get; set; } = string.Empty;
		private string? ApiResponse { get; set; }
		private bool IsLoading { get; set; } = false;

		private async void GetAnswerAsync()
		{
			IsLoading = true;
			StateHasChanged();

			try
			{
				var searchRequestDto = new AnswerRequestDto
				{
					Question = UserInput
				};

				var response = await HttpClient.PostAsJsonAsync(new Uri("api/Search/answer", UriKind.Relative), searchRequestDto);

				if (response.IsSuccessStatusCode)
				{
					var searchResult = await response.Content.ReadFromJsonAsync<SearchResultDto>();

					if (searchResult is not null)
					{
						ApiResponse = searchResult.Answer;
					}
				}
			}
			catch (Exception)
			{
				//TODO
				//Toaster error handling
				ApiResponse = "Hiba a válaszadás során.";
			}
			finally
			{
				IsLoading = false;
				StateHasChanged();
			}
		}
	}
}
