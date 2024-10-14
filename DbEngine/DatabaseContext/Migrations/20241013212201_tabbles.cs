using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class tabbles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "meta");

            migrationBuilder.EnsureSchema(
                name: "dict");

            migrationBuilder.CreateTable(
                name: "OrderPriority",
                schema: "meta",
                columns: table => new
                {
                    OrderPriorityId = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriority", x => x.OrderPriorityId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                schema: "meta",
                columns: table => new
                {
                    OrderStatusId = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "dict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TitleName = table.Column<string>(type: "text", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TimeForWork = table.Column<int>(type: "integer", nullable: false),
                    UserIdCreated = table.Column<int>(type: "integer", nullable: false),
                    UserIdAssigner = table.Column<int>(type: "integer", nullable: true),
                    OrderStatusId = table.Column<string>(type: "text", nullable: true),
                    OrderPriorityId = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_OrderPriority_OrderPriorityId",
                        column: x => x.OrderPriorityId,
                        principalSchema: "meta",
                        principalTable: "OrderPriority",
                        principalColumn: "OrderPriorityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalSchema: "meta",
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_UserIdAssigner",
                        column: x => x.UserIdAssigner,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_UserIdCreated",
                        column: x => x.UserIdCreated,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "dict",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActived = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDelete = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dict",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignersRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignersRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignersRequests_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignersRequests_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderScores",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "RequestsToCancellation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    InitiatorUserId = table.Column<int>(type: "integer", nullable: false),
                    ConfirmedUserId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsToCancellation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestsToCancellation_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "dbo",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestsToCancellation_User_ConfirmedUserId",
                        column: x => x.ConfirmedUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestsToCancellation_User_InitiatorUserId",
                        column: x => x.InitiatorUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "meta",
                table: "OrderPriority",
                columns: new[] { "OrderPriorityId", "Description" },
                values: new object[,]
                {
                    { "H", "Высокий" },
                    { "L", "Низкий" },
                    { "M", "Средний" }
                });

            migrationBuilder.InsertData(
                schema: "meta",
                table: "OrderStatus",
                columns: new[] { "OrderStatusId", "Description" },
                values: new object[,]
                {
                    { "C", "Отменен" },
                    { "D", "Утверждение иполнителя" },
                    { "F", "Завершен" },
                    { "N", "Новый" },
                    { "S", "Начат" },
                    { "X", "Удален" }
                });

            migrationBuilder.InsertData(
                schema: "dict",
                table: "Role",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "USER" },
                    { 2, "ADMIN" },
                    { 3, "MODERATOR" },
                    { 4, "GUEST" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "alexy.alexy98@mail.ru", "admin", "admin", "79518306637" },
                    { 2, "alex.alexy98@mail.ru", "user", "user", "88005553535" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserProfile",
                columns: new[] { "Id", "Birthday", "DateCreatedAt", "DateDelete", "DateUpdatedAt", "FirstName", "IsActived", "LastName", "MiddleName", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4899), null, new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4900), "Alexey", true, "Franchuk", "Dmitrievich", 1 },
                    { 2, new DateTime(1970, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4902), null, new DateTime(2024, 10, 13, 21, 21, 59, 822, DateTimeKind.Utc).AddTicks(4903), "Igor", true, "Menschin", "Vasilievich", 2 }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRole",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 1, 2 },
                    { 6, 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignersRequests_OrderId",
                schema: "dbo",
                table: "AssignersRequests",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignersRequests_UserId",
                schema: "dbo",
                table: "AssignersRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderPriorityId",
                schema: "dbo",
                table: "Order",
                column: "OrderPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusId",
                schema: "dbo",
                table: "Order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserIdAssigner",
                schema: "dbo",
                table: "Order",
                column: "UserIdAssigner");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserIdCreated",
                schema: "dbo",
                table: "Order",
                column: "UserIdCreated");

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

            migrationBuilder.CreateIndex(
                name: "IX_RequestsToCancellation_ConfirmedUserId",
                schema: "dbo",
                table: "RequestsToCancellation",
                column: "ConfirmedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsToCancellation_InitiatorUserId",
                schema: "dbo",
                table: "RequestsToCancellation",
                column: "InitiatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsToCancellation_OrderId",
                schema: "dbo",
                table: "RequestsToCancellation",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                schema: "dbo",
                table: "UserProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "dbo",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "dbo",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignersRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OrderScores",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RequestsToCancellation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "UserProfile",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "dict");

            migrationBuilder.DropTable(
                name: "OrderPriority",
                schema: "meta");

            migrationBuilder.DropTable(
                name: "OrderStatus",
                schema: "meta");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");
        }
    }
}
