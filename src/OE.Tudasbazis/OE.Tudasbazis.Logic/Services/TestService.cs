using Microsoft.EntityFrameworkCore;

using OE.Tudasbazis.DataAccess;

namespace OE.Tudasbazis.Logic.Services
{
	public class TestService
	{
		private readonly AppDbContext _context;

		public TestService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<string> Get()
		{
			var result = await _context.QuestionAnswerLogs
				.FirstOrDefaultAsync();

			return result?.Question ?? string.Empty;
		}
	}
}
