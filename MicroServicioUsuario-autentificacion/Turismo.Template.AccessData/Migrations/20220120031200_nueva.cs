using Microsoft.EntityFrameworkCore.Migrations;

namespace Turismo.Template.AccessData.Migrations
{
    public partial class nueva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Apellido", "Email", "Nombre", "Password", "RollId" },
                values: new object[] { 1, "Apellido", "admin@admin.com", "Nombre", "1234", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
