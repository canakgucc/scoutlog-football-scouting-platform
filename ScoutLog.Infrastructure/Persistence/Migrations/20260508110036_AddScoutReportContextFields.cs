using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoutLog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddScoutReportContextFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Competition",
                table: "ScoutReports",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EventDate",
                table: "ScoutReports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinutesPlayed",
                table: "ScoutReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservedPosition",
                table: "ScoutReports",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Opponent",
                table: "ScoutReports",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "ScoutReports",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Match");

            migrationBuilder.Sql("UPDATE ScoutReports SET EventDate = CreatedAt WHERE EventDate IS NULL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EventDate",
                table: "ScoutReports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 72, "Winger", "Galatasaray U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2001,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 76, "Goalkeeper", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2002,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 77, "Centre Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2003,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 78, "Centre Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2004,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), 79, "Left Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2005,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, "Right Back", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2006,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), 81, "Defensive Midfielder", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2007,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), 82, "Central Midfielder", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2008,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), 83, "Attacking Midfielder", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2009,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 84, "Winger", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2010,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, "Winger", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2011,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 86, "Striker", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2012,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), 87, "Striker", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2013,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), 88, "Goalkeeper", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2014,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 89, "Centre Back", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2015,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, "Defensive Midfielder", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2016,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), 55, "Central Midfielder", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2017,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), 56, "Attacking Midfielder", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2018,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), 57, "Winger", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2019,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), 58, "Winger", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2020,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), null, "Striker", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2021,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), 60, "Left Back", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2022,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), 61, "Right Back", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2023,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), 62, "Central Midfielder", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2024,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), 63, "Striker", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2025,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), null, "Attacking Midfielder", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2026,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), 65, "Winger", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2027,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), 66, "Winger", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2028,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), 67, "Striker", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2029,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 68, "Central Midfielder", "Başakşehir U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2030,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), null, "Attacking Midfielder", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2031,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 70, "Winger", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2032,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), 71, "Centre Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2033,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), 72, "Defensive Midfielder", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2034,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 2, 4, 0, 0, 0, 0, DateTimeKind.Utc), 73, "Left Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2035,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Training", new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, "Central Midfielder", null, "Training" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2036,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 2, 6, 0, 0, 0, 0, DateTimeKind.Utc), 75, "Striker", "Trabzonspor U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2037,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "U19 Elite League", new DateTime(2026, 2, 7, 0, 0, 0, 0, DateTimeKind.Utc), 76, "Right Back", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2038,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Utc), 77, "Central Midfielder", "Kasımpaşa U19", "Match" });

            migrationBuilder.UpdateData(
                table: "ScoutReports",
                keyColumn: "Id",
                keyValue: 2039,
                columns: new[] { "Competition", "EventDate", "MinutesPlayed", "ObservedPosition", "Opponent", "ReportType" },
                values: new object[] { "Academy Friendly", new DateTime(2026, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), 78, "Winger", "Trabzonspor U19", "Match" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competition",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "MinutesPlayed",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "ObservedPosition",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "Opponent",
                table: "ScoutReports");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "ScoutReports");
        }
    }
}
