using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Colex.Migrations
{
    /// <inheritdoc />
    public partial class Recriacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Capacidade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CapacidadeCarga = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capacidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeFantasia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    RazaoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    EnderecoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CNPJ = table.Column<long>(type: "bigint", nullable: false),
                    InscricaoEstadual = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Telefone = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarcaExtintor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaExtintor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Representante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeFantasia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    RazaoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    EnderecoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CNPJ = table.Column<long>(type: "bigint", nullable: false),
                    InscricaoEstadual = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Telefone = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<long>(type: "bigint", nullable: false),
                    Login = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Lote = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Data = table.Column<DateTime>(type: "date", nullable: true),
                    Nome = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Quantidade = table.Column<float>(type: "real", nullable: true),
                    QuantidadeAtual = table.Column<float>(type: "real", nullable: true),
                    IdFornecedor = table.Column<int>(type: "integer", nullable: false),
                    Certificado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    NF = table.Column<long>(type: "bigint", nullable: true),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componente_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MateriaPrima",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LoteInterno = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Nome = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Quantidade = table.Column<float>(type: "real", nullable: true),
                    QuantidadeAtual = table.Column<float>(type: "real", nullable: true),
                    IdFornecedor = table.Column<int>(type: "integer", nullable: true),
                    Data = table.Column<DateTime>(type: "date", nullable: true),
                    NF = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Certificado = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Batelada = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaPrima", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaPrima_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Selo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<DateTime>(type: "date", nullable: false),
                    IdFornecedor = table.Column<int>(type: "integer", nullable: false),
                    NF = table.Column<long>(type: "bigint", nullable: false),
                    NumeroInicial = table.Column<long>(type: "bigint", nullable: false),
                    NumeroFinal = table.Column<long>(type: "bigint", nullable: false),
                    Quantidade = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selo_Fornecedor_IdFornecedor",
                        column: x => x.IdFornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CEP = table.Column<long>(type: "bigint", nullable: false),
                    IdRepresentante = table.Column<int>(type: "integer", nullable: false),
                    NomeFantasia = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    RazaoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    EnderecoSocial = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Bairro = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    Numero = table.Column<long>(type: "bigint", nullable: false),
                    Cidade = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    CNPJ = table.Column<long>(type: "bigint", nullable: false),
                    InscricaoEstadual = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    Telefone = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cliente_Representante_IdRepresentante",
                        column: x => x.IdRepresentante,
                        principalTable: "Representante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Extintor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroCilindro = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    IdMarcaExtintor = table.Column<int>(type: "integer", nullable: false),
                    EnsaioHidrostatico = table.Column<int>(type: "int", nullable: false),
                    ProximoEnsaioHisdrostatico = table.Column<int>(type: "int", nullable: false),
                    NBR = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    Projeto = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    NumPatrimonio = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CapacExtintora = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IdMateriaPrima = table.Column<int>(type: "integer", nullable: false),
                    IdCapacidade = table.Column<int>(type: "integer", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false),
                    Lote = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extintor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extintor_Capacidade_IdCapacidade",
                        column: x => x.IdCapacidade,
                        principalTable: "Capacidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Extintor_MarcaExtintor_IdMarcaExtintor",
                        column: x => x.IdMarcaExtintor,
                        principalTable: "MarcaExtintor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Extintor_MateriaPrima_IdMateriaPrima",
                        column: x => x.IdMateriaPrima,
                        principalTable: "MateriaPrima",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Os",
                columns: table => new
                {
                    IdOServico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataAbertura = table.Column<DateTime>(type: "date", nullable: false),
                    IdCliente = table.Column<int>(type: "integer", nullable: false),
                    NumeroOrdemServico = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Os", x => x.IdOServico);
                    table.ForeignKey(
                        name: "FK_Os_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "RelatorioItens",
                columns: table => new
                {
                    IdRelatorioItens = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdExtintor = table.Column<int>(type: "integer", nullable: false),
                    IdOServico = table.Column<int>(type: "integer", nullable: false),
                    IdComponentes1 = table.Column<int>(type: "integer", nullable: true),
                    IdComponentes2 = table.Column<int>(type: "integer", nullable: true),
                    IdComponentes3 = table.Column<int>(type: "integer", nullable: true),
                    IdComponentes4 = table.Column<int>(type: "integer", nullable: true),
                    DataProximaRecarga = table.Column<DateTime>(type: "date", nullable: false),
                    NivelManutencao = table.Column<int>(type: "int", nullable: false),
                    EnsaioIndPre = table.Column<bool>(type: "bool", nullable: false),
                    EnsaioVazVal = table.Column<bool>(type: "bool", nullable: false),
                    InspRosca = table.Column<bool>(type: "bool", nullable: false),
                    VisualIntacto = table.Column<bool>(type: "bool", nullable: false),
                    Pintura = table.Column<bool>(type: "bool", nullable: false),
                    PesoCilindroVazio = table.Column<float>(type: "real", nullable: false),
                    PesoComAgua = table.Column<float>(type: "real", nullable: false),
                    VolumeLitros = table.Column<float>(type: "real", nullable: false),
                    CapacidadeMaxima = table.Column<float>(type: "real", nullable: false),
                    PressaoTrabalho = table.Column<float>(type: "real", nullable: false),
                    PressaoTesteCilindro = table.Column<float>(type: "real", nullable: false),
                    PressaoTesteMangueira = table.Column<float>(type: "real", nullable: false),
                    PressaoTesteManometro = table.Column<float>(type: "real", nullable: false),
                    DefInstantanea = table.Column<float>(type: "real", nullable: false),
                    DefPermanente = table.Column<float>(type: "real", nullable: false),
                    PorcEpEt = table.Column<float>(type: "real", nullable: false),
                    TaraGravada = table.Column<float>(type: "real", nullable: false),
                    TaraReal = table.Column<float>(type: "real", nullable: false),
                    PerdaMassa = table.Column<float>(type: "real", nullable: false),
                    MotivoRepro = table.Column<int>(type: "int", nullable: false),
                    LaudoAR = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    NumSelo = table.Column<long>(type: "bigint", nullable: false),
                    Reaproveitado = table.Column<bool>(type: "bool", nullable: false),
                    OsIdOServico = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatorioItens", x => x.IdRelatorioItens);
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Componente_IdComponentes1",
                        column: x => x.IdComponentes1,
                        principalTable: "Componente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Componente_IdComponentes2",
                        column: x => x.IdComponentes2,
                        principalTable: "Componente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Componente_IdComponentes3",
                        column: x => x.IdComponentes3,
                        principalTable: "Componente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Componente_IdComponentes4",
                        column: x => x.IdComponentes4,
                        principalTable: "Componente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Extintor_IdExtintor",
                        column: x => x.IdExtintor,
                        principalTable: "Extintor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Os_IdOServico",
                        column: x => x.IdOServico,
                        principalTable: "Os",
                        principalColumn: "IdOServico",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_RelatorioItens_Os_OsIdOServico",
                        column: x => x.OsIdOServico,
                        principalTable: "Os",
                        principalColumn: "IdOServico");
                });

            migrationBuilder.CreateTable(
                name: "Os_Relatorio",
                columns: table => new
                {
                    IdOsRelatorio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdOServico = table.Column<int>(type: "integer", nullable: false),
                    IdRelatorioItens = table.Column<int>(type: "integer", nullable: false),
                    Relatorio = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Os_Relatorio", x => x.IdOsRelatorio);
                    table.ForeignKey(
                        name: "FK_Os_Relatorio_Os_IdOServico",
                        column: x => x.IdOServico,
                        principalTable: "Os",
                        principalColumn: "IdOServico",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Os_Relatorio_RelatorioItens_IdRelatorioItens",
                        column: x => x.IdRelatorioItens,
                        principalTable: "RelatorioItens",
                        principalColumn: "IdRelatorioItens",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdRepresentante",
                table: "Cliente",
                column: "IdRepresentante");

            migrationBuilder.CreateIndex(
                name: "IX_Componente_IdFornecedor",
                table: "Componente",
                column: "IdFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_Extintor_IdCapacidade",
                table: "Extintor",
                column: "IdCapacidade");

            migrationBuilder.CreateIndex(
                name: "IX_Extintor_IdMarcaExtintor",
                table: "Extintor",
                column: "IdMarcaExtintor");

            migrationBuilder.CreateIndex(
                name: "IX_Extintor_IdMateriaPrima",
                table: "Extintor",
                column: "IdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaPrima_IdFornecedor",
                table: "MateriaPrima",
                column: "IdFornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_Os_IdCliente",
                table: "Os",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Os_Relatorio_IdOServico",
                table: "Os_Relatorio",
                column: "IdOServico");

            migrationBuilder.CreateIndex(
                name: "IX_Os_Relatorio_IdRelatorioItens",
                table: "Os_Relatorio",
                column: "IdRelatorioItens");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdComponentes1",
                table: "RelatorioItens",
                column: "IdComponentes1");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdComponentes2",
                table: "RelatorioItens",
                column: "IdComponentes2");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdComponentes3",
                table: "RelatorioItens",
                column: "IdComponentes3");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdComponentes4",
                table: "RelatorioItens",
                column: "IdComponentes4");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdExtintor",
                table: "RelatorioItens",
                column: "IdExtintor");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_IdOServico",
                table: "RelatorioItens",
                column: "IdOServico");

            migrationBuilder.CreateIndex(
                name: "IX_RelatorioItens_OsIdOServico",
                table: "RelatorioItens",
                column: "OsIdOServico");

            migrationBuilder.CreateIndex(
                name: "IX_Selo_IdFornecedor",
                table: "Selo",
                column: "IdFornecedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Os_Relatorio");

            migrationBuilder.DropTable(
                name: "Selo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "RelatorioItens");

            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "Extintor");

            migrationBuilder.DropTable(
                name: "Os");

            migrationBuilder.DropTable(
                name: "Capacidade");

            migrationBuilder.DropTable(
                name: "MarcaExtintor");

            migrationBuilder.DropTable(
                name: "MateriaPrima");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "Representante");
        }
    }
}
