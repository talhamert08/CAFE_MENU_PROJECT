using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_Id",
                table: "ProductPropertyTable");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPropertyTable_ProductId",
                table: "ProductPropertyTable",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_ProductId",
                table: "ProductPropertyTable",
                column: "ProductId",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_ProductId",
                table: "ProductPropertyTable");

            migrationBuilder.DropIndex(
                name: "IX_ProductPropertyTable_ProductId",
                table: "ProductPropertyTable");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPropertyTable_ProductTable_Id",
                table: "ProductPropertyTable",
                column: "Id",
                principalTable: "ProductTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
