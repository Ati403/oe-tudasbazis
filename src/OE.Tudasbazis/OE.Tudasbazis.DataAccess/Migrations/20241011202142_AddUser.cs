using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OE.Tudasbazis.DataAccess.Migrations
{
	/// <inheritdoc />
	public partial class AddUser : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<Guid>(
				name: "Id",
				table: "QuestionAnswerLogs",
				type: "char(36)",
				nullable: false,
				collation: "ascii_general_ci",
				oldClrType: typeof(int),
				oldType: "int")
				.OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

			migrationBuilder.AddColumn<Guid>(
				name: "UserId",
				table: "QuestionAnswerLogs",
				type: "char(36)",
				nullable: true,
				collation: "ascii_general_ci");

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
					Username = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4"),
					Password = table.Column<string>(type: "longtext", nullable: false)
						.Annotation("MySql:CharSet", "utf8mb4")
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				})
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateIndex(
				name: "IX_QuestionAnswerLogs_UserId",
				table: "QuestionAnswerLogs",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_QuestionAnswerLogs_Users_UserId",
				table: "QuestionAnswerLogs",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_QuestionAnswerLogs_Users_UserId",
				table: "QuestionAnswerLogs");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropIndex(
				name: "IX_QuestionAnswerLogs_UserId",
				table: "QuestionAnswerLogs");

			migrationBuilder.DropColumn(
				name: "UserId",
				table: "QuestionAnswerLogs");

			migrationBuilder.AlterColumn<int>(
				name: "Id",
				table: "QuestionAnswerLogs",
				type: "int",
				nullable: false,
				oldClrType: typeof(Guid),
				oldType: "char(36)")
				.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
				.OldAnnotation("Relational:Collation", "ascii_general_ci");
		}
	}
}
