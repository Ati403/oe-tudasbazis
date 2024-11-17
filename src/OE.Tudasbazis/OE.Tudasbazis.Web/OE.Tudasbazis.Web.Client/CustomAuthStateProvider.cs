using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
	private readonly ILocalStorageService _localStorage;

	public CustomAuthStateProvider(ILocalStorageService localStorage)
	{
		_localStorage = localStorage;
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		string? token = await _localStorage.GetItemAsync<string>("TOKEN");

		if (string.IsNullOrEmpty(token))
		{
			return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
		}

		var handler = new JwtSecurityTokenHandler();
		var jwt = handler.ReadJwtToken(token);

		var identity = new ClaimsIdentity(jwt.Claims, "jwt");
		var user = new ClaimsPrincipal(identity);

		return new AuthenticationState(user);
	}

	public void MarkUserAsAuthenticated(string username)
	{
		var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth_type");
		var user = new ClaimsPrincipal(identity);

		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}

	public async void MarkUserAsLoggedOut()
	{
		await _localStorage.RemoveItemAsync("TOKEN");

		var identity = new ClaimsIdentity();
		var user = new ClaimsPrincipal(identity);

		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}
}