using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OE.Tudasbazis.DataAccess.Migrations
{
	/// <inheritdoc />
	public partial class QuestionAnswerIdFix : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
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

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
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
		}
	}
}
