using Microsoft.EntityFrameworkCore;
using OE.Tudasbazis.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
