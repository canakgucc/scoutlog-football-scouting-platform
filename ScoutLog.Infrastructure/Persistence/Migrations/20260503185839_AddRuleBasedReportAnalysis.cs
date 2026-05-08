using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRuleBasedReportAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DevelopmentAdvice",
                table: "ScoutReports",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SuggestedScore",
                table: "ScoutReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "ScoutReports",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DevelopmentAdvice", "SuggestedScore", "Tags" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevelopmentAdvice",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "SuggestedScore",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "ScoutReports");
        }
    }
}
