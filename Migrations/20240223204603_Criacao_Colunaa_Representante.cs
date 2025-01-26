using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoColunaaRepresentante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRepresentante",
                table: "Os",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Os_IdRepresentante",
                table: "Os",
                column: "IdRepresentante");

            migrationBuilder.AddForeignKey(
                name: "FK_Os_Representante_IdRepresentante",
                table: "Os",
                column: "IdRepresentante",
                principalTable: "Representante",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Os_Representante_IdRepresentante",
                table: "Os");

            migrationBuilder.DropIndex(
                name: "IX_Os_IdRepresentante",
                table: "Os");

            migrationBuilder.DropColumn(
                name: "IdRepresentante",
                table: "Os");
        }
    }
}
