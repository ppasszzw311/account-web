using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_web.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 793, DateTimeKind.Local).AddTicks(3310), new DateTime(2025, 7, 27, 18, 36, 31, 793, DateTimeKind.Local).AddTicks(4160) });

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 793, DateTimeKind.Local).AddTicks(4880), new DateTime(2025, 7, 27, 18, 36, 31, 793, DateTimeKind.Local).AddTicks(4880) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(110), new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(140) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(170), new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(180) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "RefreshToken", "RefreshTokenExpiryTime", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5100), null, null, new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "RefreshToken", "RefreshTokenExpiryTime", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5130), null, null, new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5130) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 612, DateTimeKind.Local).AddTicks(9023), new DateTime(2025, 7, 25, 12, 44, 20, 612, DateTimeKind.Local).AddTicks(9192) });

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 612, DateTimeKind.Local).AddTicks(9300), new DateTime(2025, 7, 25, 12, 44, 20, 612, DateTimeKind.Local).AddTicks(9301) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3191), new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3191) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3195), new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3196) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3834), new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3835) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3838), new DateTime(2025, 7, 25, 12, 44, 20, 613, DateTimeKind.Local).AddTicks(3839) });
        }
    }
}
