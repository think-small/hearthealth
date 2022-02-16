using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeartHealth.Infrastructure.Migrations
{
    public partial class AddRequiresVerificationToMeasurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "RequiresVerification",
                table: "Measurements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "RequiresVerification", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "f38ebfbb-fe5d-4a51-b709-df6cca17c6f8", null, false, new DateTime(2022, 2, 13, 23, 19, 47, 934, DateTimeKind.Utc).AddTicks(636), 80, 120 });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "RequiresVerification", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "4fba6019-470a-4d12-b268-cd87b614cb83", null, false, new DateTime(2022, 2, 12, 23, 19, 47, 934, DateTimeKind.Utc).AddTicks(648), 77, 112 });

            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "HistoryId", "RequiresVerification", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "26aecce0-383f-440b-9b56-1b5004a70ab7", null, false, new DateTime(2022, 2, 11, 23, 19, 47, 934, DateTimeKind.Utc).AddTicks(721), 81, 118 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "26aecce0-383f-440b-9b56-1b5004a70ab7");

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "4fba6019-470a-4d12-b268-cd87b614cb83");

            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "f38ebfbb-fe5d-4a51-b709-df6cca17c6f8");

            migrationBuilder.DropColumn(
                name: "RequiresVerification",
                table: "Measurements");

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
    }
}
