using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class CraindoTabelaOrcamentoOramcentoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orcamento",
                columns: table => new
                {
                    IdOrcamento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    Cep = table.Column<long>(type: "bigint", nullable: true),
                    Endereco = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Cidade = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cnpj = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cpf = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Estado = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Comprador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Vendedor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: true),
                    DataModificacao = table.Column<DateTime>(type: "date", nullable: true),
                    Nota = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Observacao = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ValorFinal = table.Column<decimal>(type: "decimal", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orcamento", x => x.IdOrcamento);
                    table.ForeignKey(
                        name: "FK_Orcamento_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrcamentoProduto",
                columns: table => new
                {
                    IdOrcamentoProduto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    IdOrcamento = table.Column<int>(type: "integer", nullable: false),
                    IdMateriaPrima = table.Column<int>(type: "integer", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: true),
                    ValorUnitario = table.Column<decimal>(type: "decimal", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentoProduto", x => x.IdOrcamentoProduto);
                    table.ForeignKey(
                        name: "FK_OrcamentoProduto_MateriaPrima_IdMateriaPrima",
                        column: x => x.IdMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrcamentoProduto_Orcamento_IdOrcamento",
                        column: x => x.IdOrcamento,
                        principalTable: "Orcamento",
                        principalColumn: "IdOrcamento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orcamento_IdCliente",
                table: "Orcamento",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoProduto_IdMateriaPrima",
                table: "OrcamentoProduto",
                column: "IdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoProduto_IdOrcamento",
                table: "OrcamentoProduto",
                column: "IdOrcamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrcamentoProduto");

            migrationBuilder.DropTable(
                name: "Orcamento");
        }
    }
}
