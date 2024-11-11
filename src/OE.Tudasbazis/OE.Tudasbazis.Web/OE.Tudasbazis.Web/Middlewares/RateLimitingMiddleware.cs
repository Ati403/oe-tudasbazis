using Microsoft.Extensions.Caching.Memory;

using OE.Tudasbazis.Web.Controllers;

namespace OE.Tudasbazis.Web.Middlewares
{
	public static class RateLimitingHelper
	{
		public static List<string> LimitedEndpoints = new List<string>
		{
			nameof(SearchController.GetAnswer)
		};
	}

	public class RateLimitingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IMemoryCache _chache;

		public RateLimitingMiddleware(RequestDelegate next, IMemoryCache chache)
		{
			_next = next;
			_chache = chache;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var endpoint = context.GetEndpoint();

			if (endpoint == null
				|| !RateLimitingHelper.LimitedEndpoints.Exists(endpoint.DisplayName!.Contains))
			{
				await _next(context);
				return;
			}

			bool isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
			if (isAuthenticated)
			{
				await _next(context);
				return;
			}

			string? ipAddress = context.Connection.RemoteIpAddress?.ToString();
			if (ipAddress is null)
			{
				context.Response.StatusCode = StatusCodes.Status403Forbidden;
				await context.Response.WriteAsync("IP address not found.");
				return;
			}

			string cacheKey = $"RateLimit_{ipAddress}";
			if (_chache.TryGetValue(cacheKey, out bool hasAccessed))
			{
				context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
				await context.Response.WriteAsync("Be kell jelentkezni további kérések küldéséhez.");
				return;
			}

			_chache.Set(cacheKey, true, new MemoryCacheEntryOptions
			{
				Priority = CacheItemPriority.Normal,
				SlidingExpiration = TimeSpan.FromMinutes(10)
			});

			await _next(context);
		}
	}
}
