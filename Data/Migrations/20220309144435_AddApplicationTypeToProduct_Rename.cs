using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddApplicationTypeToProduct_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_ApplicationType_ApplicationId",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "product",
                newName: "ApplicationTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_product_ApplicationId",
                table: "product",
                newName: "IX_product_ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_ApplicationType_ApplicationTypeId",
                table: "product",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_ApplicationType_ApplicationTypeId",
                table: "product");

            migrationBuilder.RenameColumn(
                name: "ApplicationTypeId",
                table: "product",
                newName: "ApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_product_ApplicationTypeId",
                table: "product",
                newName: "IX_product_ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_ApplicationType_ApplicationId",
                table: "product",
                column: "ApplicationId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
