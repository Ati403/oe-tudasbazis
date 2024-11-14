using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

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

		// Create claims based on the token and create a ClaimsPrincipal
		var claims = new List<Claim>
		{
			// Add your claims here, for example:
			// new Claim(ClaimTypes.Name, "username from token"),
		};
		var identity = new ClaimsIdentity(claims, "jwt");
		var user = new ClaimsPrincipal(identity);

		return new AuthenticationState(user);
	}

	public void MarkUserAsAuthenticated(string username)
	{
		var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }, "apiauth_type");
		var user = new ClaimsPrincipal(identity);

		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}

	public void MarkUserAsLoggedOut()
	{
		var identity = new ClaimsIdentity();
		var user = new ClaimsPrincipal(identity);

		//TODO: Remove token from local storage

		NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
	}
}