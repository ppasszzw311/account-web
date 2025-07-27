using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_web.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRefreshTokenFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 370, DateTimeKind.Local).AddTicks(510), new DateTime(2025, 7, 27, 19, 6, 39, 370, DateTimeKind.Local).AddTicks(6190) });

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 371, DateTimeKind.Local).AddTicks(3340), new DateTime(2025, 7, 27, 19, 6, 39, 371, DateTimeKind.Local).AddTicks(3340) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 384, DateTimeKind.Local).AddTicks(2480), new DateTime(2025, 7, 27, 19, 6, 39, 384, DateTimeKind.Local).AddTicks(2600) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 384, DateTimeKind.Local).AddTicks(2670), new DateTime(2025, 7, 27, 19, 6, 39, 384, DateTimeKind.Local).AddTicks(2680) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 396, DateTimeKind.Local).AddTicks(9600), new DateTime(2025, 7, 27, 19, 6, 39, 396, DateTimeKind.Local).AddTicks(9700) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 19, 6, 39, 396, DateTimeKind.Local).AddTicks(9770), new DateTime(2025, 7, 27, 19, 6, 39, 396, DateTimeKind.Local).AddTicks(9780) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 203, DateTimeKind.Local).AddTicks(5550), new DateTime(2025, 7, 27, 18, 57, 24, 203, DateTimeKind.Local).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 204, DateTimeKind.Local).AddTicks(3050), new DateTime(2025, 7, 27, 18, 57, 24, 204, DateTimeKind.Local).AddTicks(3070) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 212, DateTimeKind.Local).AddTicks(3240), new DateTime(2025, 7, 27, 18, 57, 24, 212, DateTimeKind.Local).AddTicks(3350) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 212, DateTimeKind.Local).AddTicks(3420), new DateTime(2025, 7, 27, 18, 57, 24, 212, DateTimeKind.Local).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "RefreshToken", "RefreshTokenExpiryTime", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9330), null, null, new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "RefreshToken", "RefreshTokenExpiryTime", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9420), null, null, new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9430) });
        }
    }
}
