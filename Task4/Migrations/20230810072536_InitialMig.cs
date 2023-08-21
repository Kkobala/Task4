using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task4.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "IsBlocked", "IsDeleted", "PasswordHash", "RegistrationTime" },
                values: new object[] { "3fe66cc8-066d-480f-9cb6-35b294749b73", false, false, "AQAAAAIAAYagAAAAEGLbLgXoR+OGMlF3InM5cXIbECbF3yQZuTZ7bSVLqY2xqQmwB5O77Zb5IUeEsEqTAQ==", new DateTime(2023, 8, 10, 7, 25, 36, 306, DateTimeKind.Utc).AddTicks(8807) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RegistrationTime" },
                values: new object[] { "17188058-c32f-4470-98da-8fd55704368b", "AQAAAAIAAYagAAAAENBstQYrssqCI2FkR7wVjkIVuJ72xidYwODI7nFk6fGZo3qyuaLlaJLSlTEs0QilvA==", new DateTime(2023, 8, 9, 12, 29, 30, 266, DateTimeKind.Utc).AddTicks(3595) });
        }
    }
}
