using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowYourPostTaxes.Data.Migrations
{
    public partial class RemoveTaxName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Taxes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Taxes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
