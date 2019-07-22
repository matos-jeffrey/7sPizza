using Microsoft.EntityFrameworkCore.Migrations;

namespace SevensPizzaEntity.Migrations
{
    public partial class TeamMerge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PizzaName",
                table: "Pizzas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CardID",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Checkout",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "CreditCards",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CardID",
                table: "Orders",
                column: "CardID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders",
                column: "CardID",
                principalTable: "CreditCards",
                principalColumn: "CardID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CreditCards_CardID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CardID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PizzaName",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "CardID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Checkout",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "CreditCards");
        }
    }
}
