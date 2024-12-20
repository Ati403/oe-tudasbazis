using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OE.Tudasbazis.DataAccess.Migrations
{
	/// <inheritdoc />
	public partial class Init : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterDatabase()
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateTable(
				name: "QuestionAnswerLogs",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
					Question = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Answer = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_QuestionAnswerLogs", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "QuestionAnswerLogs");
		}
	}
}
