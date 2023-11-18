using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstacionamentoBackoffice.Data.Migrations
{
    public partial class AddMarcaParaCarro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Carros",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Carros");
        }
    }
}
