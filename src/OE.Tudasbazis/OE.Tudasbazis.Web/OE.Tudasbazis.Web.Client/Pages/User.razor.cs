using System.Net.Http.Json;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using OE.Tudasbazis.Application.DTOs.Requests;

using OE.Tudasbazis.Application.DTOs.Responses;

namespace OE.Tudasbazis.Web.Client.Pages
{
	public partial class User : ComponentBase
	{
		private LoginOrRegisterRequestDto LoginRequestDto { get; set; } = new();
		private bool IsLoginLoading { get; set; } = false;

		private RegisterModelTemp RegisterModel { get; set; } = new();
		private LoginOrRegisterRequestDto RegisterRequestDto { get; set; } = new();
		private bool IsRegisterLoading { get; set; } = false;

		[CascadingParameter]
		public Task<AuthenticationState> AuthTask { get; set; }

		private async Task HandleLoginAsync()
		{
			IsLoginLoading = true;
			StateHasChanged();

			try
			{
				var result = await Http.PostAsJsonAsync<LoginOrRegisterRequestDto>("api/Auth/Login", LoginRequestDto);

				if (result.IsSuccessStatusCode)
				{
					var jwtDto = await result.Content.ReadFromJsonAsync<JwtDto>();

					if (jwtDto is not null)
					{
						await LocalStorage.SetItemAsync("TOKEN", jwtDto.Token);
						if (AuthStateProvider is CustomAuthStateProvider customProvider)
						{
							customProvider.MarkUserAsAuthenticated(jwtDto.Token);
						}
					}
				}
				else
				{
					if (AuthStateProvider is CustomAuthStateProvider customProvider)
					{
						customProvider.MarkUserAsLoggedOut();
					}

					await HandleError(result);
				}
			}
			catch (Exception)
			{
				Toaster.ShowError("Hiba a bejelenkezés során, próbáld meg később.");
			}

			await CheckAuthState();

			IsLoginLoading = false;
			LoginRequestDto = new();
			StateHasChanged();
		}

		private async Task CheckAuthState()
		{
			try
			{
				var authState = await AuthStateProvider.GetAuthenticationStateAsync();
				var user = authState.User;

				if (user.Identity != null && user.Identity.IsAuthenticated)
				{
					Toaster.ShowSuccess($"Bejelentkezve {user.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value}-ként.");
				}
				else
				{
					Toaster.ShowWarning("Sikertelen azonosítás.");
				}
			}
			catch (Exception)
			{
				Toaster.ShowError($"Hiba az azonosítás során.");
			}
		}

		private async Task HandleRegisterAsync()
		{
			bool isRegisterModelValid = ValidateRegisterModel();

			if (!isRegisterModelValid)
			{
				Toaster.ShowWarning("A jelszavak nem egyeznek.");
				return;
			}

			IsRegisterLoading = true;
			StateHasChanged();

			try
			{
				RegisterRequestDto.Username = RegisterModel.Username;
				RegisterRequestDto.Password = RegisterModel.Password;

				var result = await Http.PostAsJsonAsync<LoginOrRegisterRequestDto>("api/Auth/Register", RegisterRequestDto);

				if (result.IsSuccessStatusCode)
				{
					Toaster.ShowSuccess("Sikeres regisztráció.");
				}
				else
				{
					await HandleError(result);
				}
			}
			catch (Exception)
			{
				Toaster.ShowError("Hiba a regisztráció során, próbáld meg később.");
			}

			IsRegisterLoading = false;
			RegisterModel = new();
			StateHasChanged();
		}

		private bool ValidateRegisterModel() => RegisterModel.Password == RegisterModel.PasswordAgain;

		private async Task HandleError(HttpResponseMessage? httpResponse)
		{
			var error = await httpResponse.Content.ReadFromJsonAsync<ErrorResponseDto>();

			string errorsConcated = string.Join(", ", error?.Errors ?? []);

			Toaster.ShowWarning(errorsConcated);
		}
	}

	public class RegisterModelTemp
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string PasswordAgain { get; set; } = string.Empty;
	}
}
