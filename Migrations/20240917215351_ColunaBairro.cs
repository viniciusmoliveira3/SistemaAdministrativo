using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class ColunaBairro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Orcamento",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Orcamento");
        }
    }
}
