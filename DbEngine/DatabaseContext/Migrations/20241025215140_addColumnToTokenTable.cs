using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class addColumnToTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_User_UserId",
                schema: "dbo",
                table: "Token");

            migrationBuilder.AddColumn<string>(
                name: "TokenValue",
                schema: "dbo",
                table: "Token",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 21, 51, 39, 853, DateTimeKind.Utc).AddTicks(7504), new DateTime(2024, 10, 25, 21, 51, 39, 853, DateTimeKind.Utc).AddTicks(7506) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 25, 21, 51, 39, 853, DateTimeKind.Utc).AddTicks(7508), new DateTime(2024, 10, 25, 21, 51, 39, 853, DateTimeKind.Utc).AddTicks(7509) });

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_UserId",
                schema: "dbo",
                table: "Token",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Token_User_UserId",
                schema: "dbo",
                table: "Token");

            migrationBuilder.DropColumn(
                name: "TokenValue",
                schema: "dbo",
                table: "Token");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 14, 22, 53, 41, 296, DateTimeKind.Utc).AddTicks(3457), new DateTime(2024, 10, 14, 22, 53, 41, 296, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 14, 22, 53, 41, 296, DateTimeKind.Utc).AddTicks(3467), new DateTime(2024, 10, 14, 22, 53, 41, 296, DateTimeKind.Utc).AddTicks(3468) });

            migrationBuilder.AddForeignKey(
                name: "FK_Token_User_UserId",
                schema: "dbo",
                table: "Token",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
