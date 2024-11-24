using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class Add_new_Column_in_Order_Tabble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactInformation",
                schema: "dbo",
                table: "Order",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 24, 1, 8, 15, 999, DateTimeKind.Utc).AddTicks(9674), new DateTime(2024, 11, 24, 1, 8, 15, 999, DateTimeKind.Utc).AddTicks(9677) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 24, 1, 8, 15, 999, DateTimeKind.Utc).AddTicks(9679), new DateTime(2024, 11, 24, 1, 8, 15, 999, DateTimeKind.Utc).AddTicks(9679) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInformation",
                schema: "dbo",
                table: "Order");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 17, 20, 23, 15, 733, DateTimeKind.Utc).AddTicks(7425), new DateTime(2024, 11, 17, 20, 23, 15, 733, DateTimeKind.Utc).AddTicks(7428) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 11, 17, 20, 23, 15, 733, DateTimeKind.Utc).AddTicks(7430), new DateTime(2024, 11, 17, 20, 23, 15, 733, DateTimeKind.Utc).AddTicks(7430) });
        }
    }
}
