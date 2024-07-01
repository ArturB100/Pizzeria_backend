using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class OrderAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaOrderId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PizzaOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaOrder_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaOrder_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_PizzaOrderId",
                table: "Pizza",
                column: "PizzaOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaOrder_AddressId",
                table: "PizzaOrder",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaOrder_UserId",
                table: "PizzaOrder",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_PizzaOrder_PizzaOrderId",
                table: "Pizza",
                column: "PizzaOrderId",
                principalTable: "PizzaOrder",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_PizzaOrder_PizzaOrderId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "PizzaOrder");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_PizzaOrderId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "PizzaOrderId",
                table: "Pizza");
        }
    }
}
