using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class TirandoObrigatoriedadeColuna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentoProduto_MateriaPrima_IdMateriaPrima",
                table: "OrcamentoProduto");

            migrationBuilder.AlterColumn<int>(
                name: "IdMateriaPrima",
                table: "OrcamentoProduto",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentoProduto_MateriaPrima_IdMateriaPrima",
                table: "OrcamentoProduto",
                column: "IdMateriaPrima",
                principalTable: "MateriaPrima",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrcamentoProduto_MateriaPrima_IdMateriaPrima",
                table: "OrcamentoProduto");

            migrationBuilder.AlterColumn<int>(
                name: "IdMateriaPrima",
                table: "OrcamentoProduto",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrcamentoProduto_MateriaPrima_IdMateriaPrima",
                table: "OrcamentoProduto",
                column: "IdMateriaPrima",
                principalTable: "MateriaPrima",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
