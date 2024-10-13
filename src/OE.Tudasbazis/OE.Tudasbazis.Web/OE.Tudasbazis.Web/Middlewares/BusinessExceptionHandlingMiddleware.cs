using OE.Tudasbazis.Application.Exceptions;

namespace OE.Tudasbazis.Web.Middlewares
{
	public class BusinessExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public BusinessExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (BusinessLogicException ex)
			{
				context.Response.StatusCode = ex.StatusCode;
				await context.Response.WriteAsJsonAsync(new { message = ex.Message });
			}
#if !DEBUG
			catch (Exception)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsJsonAsync(new { message = "An unexpected error occured." });
			}
#endif
		}
	}
}