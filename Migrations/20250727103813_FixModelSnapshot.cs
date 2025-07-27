using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_web.Migrations
{
    /// <inheritdoc />
    public partial class FixModelSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 733, DateTimeKind.Local).AddTicks(6260), new DateTime(2025, 7, 27, 18, 38, 10, 733, DateTimeKind.Local).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "Factories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 733, DateTimeKind.Local).AddTicks(8860), new DateTime(2025, 7, 27, 18, 38, 10, 733, DateTimeKind.Local).AddTicks(8860) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 737, DateTimeKind.Local).AddTicks(3340), new DateTime(2025, 7, 27, 18, 38, 10, 737, DateTimeKind.Local).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 737, DateTimeKind.Local).AddTicks(3420), new DateTime(2025, 7, 27, 18, 38, 10, 737, DateTimeKind.Local).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 738, DateTimeKind.Local).AddTicks(1970), new DateTime(2025, 7, 27, 18, 38, 10, 738, DateTimeKind.Local).AddTicks(1990) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 38, 10, 738, DateTimeKind.Local).AddTicks(2020), new DateTime(2025, 7, 27, 18, 38, 10, 738, DateTimeKind.Local).AddTicks(2020) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5100), new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5120) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5130), new DateTime(2025, 7, 27, 18, 36, 31, 796, DateTimeKind.Local).AddTicks(5130) });
        }
    }
}
