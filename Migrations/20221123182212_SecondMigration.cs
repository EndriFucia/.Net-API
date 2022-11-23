using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ListProducts",
                table: "ListProducts");

            migrationBuilder.RenameTable(
                name: "ListProducts",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ListProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListProducts",
                table: "ListProducts",
                column: "Id");
        }
    }
}
