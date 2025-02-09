using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopLists_Ingredients_IngredientId",
                table: "ShopLists");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopLists_Users_UserId",
                table: "ShopLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopLists",
                table: "ShopLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient");

            migrationBuilder.RenameTable(
                name: "ShopLists",
                newName: "shop_list");

            migrationBuilder.RenameTable(
                name: "RecipeIngredient",
                newName: "recipe_ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_ShopLists_UserId",
                table: "shop_list",
                newName: "IX_shop_list_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopLists_IngredientId",
                table: "shop_list",
                newName: "IX_shop_list_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeIngredient_IngredientId",
                table: "recipe_ingredient",
                newName: "IX_recipe_ingredient_IngredientId");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RecipeUrl",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shop_list",
                table: "shop_list",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipe_ingredient",
                table: "recipe_ingredient",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_ingredient_Ingredients_IngredientId",
                table: "recipe_ingredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_recipe_ingredient_Recipes_RecipeId",
                table: "recipe_ingredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_list_Ingredients_IngredientId",
                table: "shop_list",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shop_list_Users_UserId",
                table: "shop_list",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recipe_ingredient_Ingredients_IngredientId",
                table: "recipe_ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_recipe_ingredient_Recipes_RecipeId",
                table: "recipe_ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_list_Ingredients_IngredientId",
                table: "shop_list");

            migrationBuilder.DropForeignKey(
                name: "FK_shop_list_Users_UserId",
                table: "shop_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shop_list",
                table: "shop_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_recipe_ingredient",
                table: "recipe_ingredient");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "RecipeUrl",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "shop_list",
                newName: "ShopLists");

            migrationBuilder.RenameTable(
                name: "recipe_ingredient",
                newName: "RecipeIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_shop_list_UserId",
                table: "ShopLists",
                newName: "IX_ShopLists_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_shop_list_IngredientId",
                table: "ShopLists",
                newName: "IX_ShopLists_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_recipe_ingredient_IngredientId",
                table: "RecipeIngredient",
                newName: "IX_RecipeIngredient_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopLists",
                table: "ShopLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipeIngredient",
                table: "RecipeIngredient",
                columns: new[] { "RecipeId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Ingredients_IngredientId",
                table: "RecipeIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredient_Recipes_RecipeId",
                table: "RecipeIngredient",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopLists_Ingredients_IngredientId",
                table: "ShopLists",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopLists_Users_UserId",
                table: "ShopLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
