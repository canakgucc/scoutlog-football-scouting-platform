using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    City = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchName = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    MatchDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    TechnicalScore = table.Column<int>(type: "int", nullable: false),
                    PhysicalScore = table.Column<int>(type: "int", nullable: false),
                    TacticalScore = table.Column<int>(type: "int", nullable: false),
                    MentalScore = table.Column<int>(type: "int", nullable: false),
                    OverallScore = table.Column<int>(type: "int", nullable: false),
                    CoachNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    PreferredFoot = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ScoutReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    ObservationText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicalScore = table.Column<int>(type: "int", nullable: false),
                    PhysicalScore = table.Column<int>(type: "int", nullable: false),
                    TacticalScore = table.Column<int>(type: "int", nullable: false),
                    MentalScore = table.Column<int>(type: "int", nullable: false),
                    PotentialScore = table.Column<int>(type: "int", nullable: false),
                    OverallScore = table.Column<int>(type: "int", nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    AnalysisSummary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strengths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weaknesses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggestedActions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    ScoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScoutReports_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Season = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "LogoUrl", "Name" },
                values: new object[] { 1, "Istanbul", "Turkey", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/160x160?text=SL", "ScoutLog Academy" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "System administrator", "Admin" },
                    { 2, "Club manager", "ClubManager" },
                    { 3, "Team coach", "Coach" },
                    { 4, "Scout user", "Scout" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "AgeGroup", "ClubId", "CoachId", "Name", "Season" },
                values: new object[] { 1, "U19", 1, null, "U19 Elite", "2025/26" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClubId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@scoutlog.local", "Admin User", true, "DemoPasswordHash", 1, null },
                    { 2, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "manager@scoutlog.local", "Club Manager", true, "DemoPasswordHash", 2, null }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BirthDate", "ClubId", "FirstName", "Height", "LastName", "Nationality", "PhotoUrl", "Position", "PreferredFoot", "Status", "TeamId", "Weight" },
                values: new object[] { 1, new DateOnly(2007, 4, 12), 1, "Emir", 178, "Yildiz", "Turkey", "https://placehold.co/320x320?text=EY", "Winger", "Right", "Active", 1, 70 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClubId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TeamId" },
                values: new object[,]
                {
                    { 3, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "coach@scoutlog.local", "Demo Coach", true, "DemoPasswordHash", 3, 1 },
                    { 4, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "scout@scoutlog.local", "Demo Scout", true, "DemoPasswordHash", 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "ScoutReports",
                columns: new[] { "Id", "AnalysisSummary", "CreatedAt", "MentalScore", "ObservationText", "OverallScore", "PhysicalScore", "PlayerId", "PotentialScore", "Recommendation", "ScoutId", "Strengths", "SuggestedActions", "TacticalScore", "TechnicalScore", "Title", "Weaknesses" },
                values: new object[] { 1, null, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 74, "Oyuncu hız, pres ve pas bağlantılarında etkili göründü. Pozisyon alma kararları zaman zaman geliştirilmeli.", 78, 82, 1, 86, "Izlenmeli", 4, null, null, 70, 78, "Initial wing prospect report", null });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceMetrics_PlayerId",
                table: "PerformanceMetrics",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClubId",
                table: "Players",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScoutReports_PlayerId",
                table: "ScoutReports",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_ScoutReports_ScoutId",
                table: "ScoutReports",
                column: "ScoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ClubId",
                table: "Teams",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachId",
                table: "Teams",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClubId",
                table: "Users",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceMetrics_Players_PlayerId",
                table: "PerformanceMetrics",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Teams_TeamId",
                table: "Players",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScoutReports_Users_ScoutId",
                table: "ScoutReports",
                column: "ScoutId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CoachId",
                table: "Teams",
                column: "CoachId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CoachId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PerformanceMetrics");

            migrationBuilder.DropTable(
                name: "ScoutReports");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Clubs");
        }
    }
}
