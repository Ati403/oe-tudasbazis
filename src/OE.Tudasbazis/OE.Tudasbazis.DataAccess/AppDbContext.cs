using Microsoft.EntityFrameworkCore;

using OE.Tudasbazis.Domain.Entities;

namespace OE.Tudasbazis.DataAccess
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<QuestionAnswerLog> QuestionAnswerLogs { get; set; }
	}
}
