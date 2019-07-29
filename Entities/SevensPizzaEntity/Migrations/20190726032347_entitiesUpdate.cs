using Microsoft.EntityFrameworkCore.Migrations;

namespace SevensPizzaEntity.Migrations
{
    public partial class entitiesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CardID",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DOE",
                table: "CreditCards",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders",
                column: "CardID",
                principalTable: "CreditCards",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "CardID",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DOE",
                table: "CreditCards",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders",
                column: "CardID",
                principalTable: "CreditCards",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
