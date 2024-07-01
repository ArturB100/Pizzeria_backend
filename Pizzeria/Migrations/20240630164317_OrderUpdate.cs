using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_PizzaOrder_PizzaOrderId",
                table: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_PizzaOrderId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "PizzaOrderId",
                table: "Pizza");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PizzaOrderId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_PizzaOrderId",
                table: "Pizza",
                column: "PizzaOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_PizzaOrder_PizzaOrderId",
                table: "Pizza",
                column: "PizzaOrderId",
                principalTable: "PizzaOrder",
                principalColumn: "Id");
        }
    }
}
