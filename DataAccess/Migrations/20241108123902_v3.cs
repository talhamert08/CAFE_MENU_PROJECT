using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductTable_CategoryId",
                table: "ProductTable",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyTable_PropertyId",
                table: "ProductPropertyTable",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_Id",
                table: "ProductPropertyTable",
                column: "Id",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPropertyTable_PropertyTable_PropertyId",
                table: "ProductPropertyTable",
                column: "PropertyId",
                principalTable: "PropertyTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable",
                column: "CategoryId",
                principalTable: "CategoryTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_Id",
                table: "ProductPropertyTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPropertyTable_PropertyTable_PropertyId",
                table: "ProductPropertyTable");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTable_CategoryTable_CategoryId",
                table: "ProductTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductTable_CategoryId",
                table: "ProductTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductPropertyTable_PropertyId",
                table: "ProductPropertyTable");
        }
    }
}
