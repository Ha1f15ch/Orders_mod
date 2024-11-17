using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class Create_table_for_Customer_and_Employer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderScores",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "TimeForWork",
                schema: "dbo",
                table: "Order",
                newName: "DayToDelay");

            migrationBuilder.CreateTable(
                name: "CustomerProfile",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProfile_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerProfile",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerProfile_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderScoresCustomer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    CustomerProfileId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderScoresCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderScoresCustomer_CustomerProfile_CustomerProfileId",
                        column: x => x.CustomerProfileId,
                        principalSchema: "dbo",
                        principalTable: "CustomerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderScoresCustomer_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderScoresEmployer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    EmployerProfileId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderScoresEmployer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderScoresEmployer_EmployerProfile_EmployerProfileId",
                        column: x => x.EmployerProfileId,
                        principalSchema: "dbo",
                        principalTable: "EmployerProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderScoresEmployer_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProfile_UserId",
                schema: "dbo",
                table: "CustomerProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfile_UserId",
                schema: "dbo",
                table: "EmployerProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderScoresCustomer_CustomerProfileId",
                schema: "dbo",
                table: "OrderScoresCustomer",
                column: "CustomerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderScoresCustomer_OrderId",
                schema: "dbo",
                table: "OrderScoresCustomer",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderScoresEmployer_EmployerProfileId",
                schema: "dbo",
                table: "OrderScoresEmployer",
                column: "EmployerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderScoresEmployer_OrderId",
                schema: "dbo",
                table: "OrderScoresEmployer",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderScoresCustomer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderScoresEmployer",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CustomerProfile",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EmployerProfile",
                schema: "dbo");

            migrationBuilder.RenameColumn(
                name: "DayToDelay",
                schema: "dbo",
                table: "Order",
                newName: "TimeForWork");

            migrationBuilder.CreateTable(
                name: "OrderScores",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderScores_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderScores_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderScores_OrderId",
                schema: "dbo",
                table: "OrderScores",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderScores_UserId",
                schema: "dbo",
                table: "OrderScores",
                column: "UserId");
        }
    }
}
