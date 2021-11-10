using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeartHealth.Infrastructure.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Measurements",
                columns: new[] { "Id", "Timestamp", "Diastolic", "Systolic" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", new DateTime(2021, 11, 8, 15, 55, 5, 169, DateTimeKind.Utc).AddTicks(2748), 80, 120 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");
        }
    }
}
