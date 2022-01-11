using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactApplication.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactFname",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Contacts");

            migrationBuilder.AddColumn<string>(
                name: "ContactFname",
                table: "Contacts",
                type: "text",
                nullable: true);
        }
    }
}
