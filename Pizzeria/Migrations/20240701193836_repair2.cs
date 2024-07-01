using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Migrations
{
    /// <inheritdoc />
    public partial class repair2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizza_IngredientsId1",
                table: "IngredientPizza");

            migrationBuilder.RenameColumn(
                name: "IngredientsId1",
                table: "IngredientPizza",
                newName: "PizzasId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_IngredientsId1",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_PizzasId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizza_PizzasId",
                table: "IngredientPizza",
                column: "PizzasId",
                principalTable: "Pizza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Pizza_PizzasId",
                table: "IngredientPizza");

            migrationBuilder.RenameColumn(
                name: "PizzasId",
                table: "IngredientPizza",
                newName: "IngredientsId1");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientPizza_PizzasId",
                table: "IngredientPizza",
                newName: "IX_IngredientPizza_IngredientsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Pizza_IngredientsId1",
                table: "IngredientPizza",
                column: "IngredientsId1",
                principalTable: "Pizza",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
