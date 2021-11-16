using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeartHealth.Infrastructure.Migrations
{
    public partial class AddHistoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HistoryId",
                table: "Measurements",
                type: "varchar(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Start = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    End = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    AverageSystolic = table.Column<int>(type: "int", nullable: true),
                    AverageDiastolic = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "Timestamp",
                value: new DateTime(2021, 11, 16, 19, 34, 17, 643, DateTimeKind.Utc).AddTicks(6315));

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_HistoryId",
                table: "Measurements",
                column: "HistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_Histories_HistoryId",
                table: "Measurements",
                column: "HistoryId",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_Histories_HistoryId",
                table: "Measurements");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_HistoryId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "HistoryId",
                table: "Measurements");

            migrationBuilder.UpdateData(
                table: "Measurements",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111",
                column: "Timestamp",
                value: new DateTime(2021, 11, 8, 15, 55, 5, 169, DateTimeKind.Utc).AddTicks(2748));
        }
    }
}
