using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeartHealth.Infrastructure.Migrations
{
    public partial class AddMeasurementSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "b04a8862-88e7-4c34-9d23-d170b8b11ea6", null, new DateTime(2021, 11, 18, 17, 54, 6, 567, DateTimeKind.Utc).AddTicks(8766), 80, 120 });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "ae2215cd-ccc7-4e5c-9c62-1bc730f35574", null, new DateTime(2021, 11, 17, 17, 54, 6, 567, DateTimeKind.Utc).AddTicks(8779), 77, 112 });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "f21e64dc-042d-44e2-abdc-44b40c0b7ac3", null, new DateTime(2021, 11, 16, 17, 54, 6, 567, DateTimeKind.Utc).AddTicks(8850), 81, 118 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "ae2215cd-ccc7-4e5c-9c62-1bc730f35574");

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "b04a8862-88e7-4c34-9d23-d170b8b11ea6");

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "f21e64dc-042d-44e2-abdc-44b40c0b7ac3");

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", null, new DateTime(2021, 11, 16, 19, 34, 17, 643, DateTimeKind.Utc).AddTicks(6315), 80, 120 });
        }
    }
}
