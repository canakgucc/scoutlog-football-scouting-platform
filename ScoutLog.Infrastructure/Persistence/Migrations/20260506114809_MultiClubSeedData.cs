using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MultiClubSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LogoUrl", "Name" },
                values: new object[] { "https://placehold.co/160x160?text=FB", "Fenerbahçe Futbol Kulübü" });

            migrationBuilder.InsertData(
                table: "Clubs",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 2, "Istanbul", "Turkey", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/160x160?text=GS", "Galatasaray Spor Kulübü" },
                    { 3, "Istanbul", "Turkey", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://placehold.co/160x160?text=BJK", "Beşiktaş Jimnastik Kulübü" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "AgeGroup", "ClubId", "CoachId", "Name", "Season" },
                values: new object[,]
                {
                    { 2, "U19", 2, null, "Galatasaray U19 Elite", "2025/26" },
                    { 3, "U19", 3, null, "Beşiktaş U19 Elite", "2025/26" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ClubId", "CreatedAt", "Email", "FullName", "IsActive", "PasswordHash", "RoleId", "TeamId" },
                values: new object[,]
                {
                    { 5, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "scout@galatasaray.local", "Galatasaray Scout", true, "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16", 4, 2 },
                    { 6, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "scout@besiktas.local", "Beşiktaş Scout", true, "sha256$588c55f3ce2b8569b153c5abbf13f9f74308b88a20017cc699b835cc93195d16", 4, 3 }
                });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1014,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1015,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1016,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1017,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1018,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1019,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1020,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1021,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1022,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1023,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1024,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 3, 3 });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2009,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2010,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2011,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2012,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2013,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2014,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2015,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2016,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2017,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2018,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2019,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2020,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2021,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2022,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2023,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2024,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2026,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2027,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2028,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2029,
                column: "ScoutId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2030,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2031,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2035,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2036,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2039,
                column: "ScoutId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Fenerbahçe U19 Elite");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "admin@fenerbahce.local", "Fenerbahçe Admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "manager@fenerbahce.local", "Fenerbahçe Manager" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "coach@fenerbahce.local", "Fenerbahçe Coach" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "scout@fenerbahce.local", "Fenerbahçe Scout" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LogoUrl", "Name" },
                values: new object[] { "https://placehold.co/160x160?text=SL", "ScoutLog Academy" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1009,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1010,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1011,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1012,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1013,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1014,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1015,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1016,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1017,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1018,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1019,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1020,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1021,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1022,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1023,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1024,
                columns: new[] { "ClubId", "TeamId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2009,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2010,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2011,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2012,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2013,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2014,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2015,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2016,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2017,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2018,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2019,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2020,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2021,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2022,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2023,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2024,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2026,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2027,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2028,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2029,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2030,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2031,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2035,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2036,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2039,
                column: "ScoutId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "U19 Elite");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clubs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "admin@scoutlog.local", "Admin User" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "manager@scoutlog.local", "Club Manager" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "coach@scoutlog.local", "Demo Coach" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Email", "FullName" },
                values: new object[] { "scout@scoutlog.local", "Demo Scout" });
        }
    }
}
