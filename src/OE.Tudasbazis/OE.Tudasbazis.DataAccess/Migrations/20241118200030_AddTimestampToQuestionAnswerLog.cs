using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OE.Tudasbazis.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampToQuestionAnswerLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "QuestionAnswerLogs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "QuestionAnswerLogs");
        }
    }
}
