using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace account_web.Migrations
{
    /// <inheritdoc />
    public partial class CreateRefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRevoked = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    RevokedBy = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IpAddress = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    UserAgent = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                });

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
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9330), new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9370) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9420), new DateTime(2025, 7, 27, 18, 57, 24, 213, DateTimeKind.Local).AddTicks(9430) });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

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
    }
}
