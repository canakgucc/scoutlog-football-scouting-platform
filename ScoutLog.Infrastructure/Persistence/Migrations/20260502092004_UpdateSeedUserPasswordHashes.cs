using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedUserPasswordHashes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "DemoPasswordHash");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "DemoPasswordHash");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "DemoPasswordHash");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "PasswordHash",
                value: "DemoPasswordHash");
        }
    }
}
