using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuintrixHomeworkPlayerMVP.Data.Migrations
{
    public partial class botOwnerIdAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Bot",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bot",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Bot");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bot");
        }
    }
}
