using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class table_token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                schema: "dbo",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateExpired = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Token_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                schema: "dbo",
                table: "Token",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token",
                schema: "dbo");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4899), new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4900) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "UserProfile",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreatedAt", "DateUpdatedAt" },
                values: new object[] { new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4902), new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4903) });
        }
    }
}
