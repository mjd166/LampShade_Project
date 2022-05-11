using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.Infrastructure.EFCore.Migrations
{
    public partial class correctcurrentcountfieldoftable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentDiscount",
                table: "InventoryOperations",
                newName: "Currentcount");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "InventoryOperations",
                newName: "OperationDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationDate",
                table: "InventoryOperations",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "Currentcount",
                table: "InventoryOperations",
                newName: "CurrentDiscount");
        }
    }
}
