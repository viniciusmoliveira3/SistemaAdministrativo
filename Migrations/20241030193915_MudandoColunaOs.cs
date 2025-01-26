using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class MudandoColunaOs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NumeroOrdemServico",
                table: "Os",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "NumeroOrdemServico",
                table: "Os",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);
        }
    }
}
