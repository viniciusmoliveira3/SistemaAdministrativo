using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class MudancaOrcamentoColunaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orcamento_Cliente_IdCliente",
                table: "Orcamento");

            migrationBuilder.DropIndex(
                name: "IX_Orcamento_IdCliente",
                table: "Orcamento");

            migrationBuilder.DropColumn(
                name: "IdCliente",
                table: "Orcamento");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Orcamento",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Orcamento");

            migrationBuilder.AddColumn<int>(
                name: "IdCliente",
                table: "Orcamento",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdCliente",
                table: "Orcamento",
                column: "IdCliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Orcamento_Cliente_IdCliente",
                table: "Orcamento",
                column: "IdCliente",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
