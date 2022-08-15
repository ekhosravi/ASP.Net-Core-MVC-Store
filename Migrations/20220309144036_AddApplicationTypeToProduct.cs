using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class AddApplicationTypeToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_product_ApplicationId",
                table: "product",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_ApplicationType_ApplicationId",
                table: "product",
                column: "ApplicationId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_ApplicationType_ApplicationId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_ApplicationId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "product");
        }
    }
}
