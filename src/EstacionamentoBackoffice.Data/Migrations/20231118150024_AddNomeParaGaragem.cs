using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstacionamentoBackoffice.Data.Migrations
{
    public partial class AddNomeParaGaragem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Garagens",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Garagens");
        }
    }
}
