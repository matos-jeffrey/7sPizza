using Microsoft.EntityFrameworkCore.Migrations;

namespace SevensPizzaEntity.Migrations
{
    public partial class CustPassField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Toppings",
                table: "Pizzas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Toppings",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customers");
        }
    }
}
