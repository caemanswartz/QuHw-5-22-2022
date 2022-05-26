using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuintrixHomeworkPlayerMVP.Data.Migrations
{
    public partial class UpdateIdenitityWithCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<uint>(
                name: "Currency",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AspNetUsers");
        }
    }
}
