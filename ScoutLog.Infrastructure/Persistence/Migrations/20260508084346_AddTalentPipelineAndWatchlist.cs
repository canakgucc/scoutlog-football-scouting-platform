using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTalentPipelineAndWatchlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PipelineStatus",
                table: "Players",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "New");

            migrationBuilder.CreateTable(
                name: "WatchlistItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchlistItems_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchlistItems_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchlistItems_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "PipelineStatus",
                value: "Recommended");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1001,
                column: "PipelineStatus",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1002,
                column: "PipelineStatus",
                value: "New");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1003,
                column: "PipelineStatus",
                value: "Under Observation");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1004,
                column: "PipelineStatus",
                value: "Follow-up Needed");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1005,
                column: "PipelineStatus",
                value: "Shortlisted");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1006,
                column: "PipelineStatus",
                value: "Recommended");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1007,
                column: "PipelineStatus",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1008,
                column: "PipelineStatus",
                value: "New");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1009,
                column: "PipelineStatus",
                value: "Under Observation");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1010,
                column: "PipelineStatus",
                value: "Follow-up Needed");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1011,
                column: "PipelineStatus",
                value: "Shortlisted");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1012,
                column: "PipelineStatus",
                value: "Recommended");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1013,
                column: "PipelineStatus",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1014,
                column: "PipelineStatus",
                value: "New");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1015,
                column: "PipelineStatus",
                value: "Under Observation");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1016,
                column: "PipelineStatus",
                value: "Follow-up Needed");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1017,
                column: "PipelineStatus",
                value: "Shortlisted");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1018,
                column: "PipelineStatus",
                value: "Recommended");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1019,
                column: "PipelineStatus",
                value: "Rejected");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1020,
                column: "PipelineStatus",
                value: "New");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1021,
                column: "PipelineStatus",
                value: "Under Observation");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1022,
                column: "PipelineStatus",
                value: "Follow-up Needed");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1023,
                column: "PipelineStatus",
                value: "Shortlisted");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1024,
                column: "PipelineStatus",
                value: "Recommended");

            migrationBuilder.InsertData(
                table: "WatchlistItems",
                columns: new[] { "Id", "ClubId", "CreatedAt", "CreatedByUserId", "PlayerId", "Priority", "Reason", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1, "High", "Kanat profili için A takım radarında tutulmalı.", new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1008, "High", "Yaratıcı orta saha profili, üst yaş grubu testine değer.", new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 1, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), 4, 1006, "Medium", "Merkez denge rolü için düzenli takip edilmeli.", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1009, "High", "Hız ve bire bir kalitesi nedeniyle tekrar izlenmeli.", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 5, 1010, "Medium", "Sol kanat final aksiyonları için takipte kalmalı.", new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 3, new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), 6, 1017, "High", "On numara profili yüksek potansiyel sinyali veriyor.", new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 3, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), 6, 1023, "High", "Liderlik ve oyun görüşü nedeniyle shortlist adayı.", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 3, new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), 6, 1019, "Medium", "Ters ayak kanat profili için düzenli takip önerilir.", new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItems_ClubId_PlayerId",
                table: "WatchlistItems",
                columns: new[] { "ClubId", "PlayerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItems_CreatedByUserId",
                table: "WatchlistItems",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchlistItems_PlayerId",
                table: "WatchlistItems",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchlistItems");

            migrationBuilder.DropColumn(
                name: "PipelineStatus",
                table: "Players");
        }
    }
}
