using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Sobrenome = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroTelefone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    HoraAbertura = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Atendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MesaId = table.Column<int>(type: "INTEGER", nullable: false),
                    GarcomId = table.Column<int>(type: "INTEGER", nullable: false),
                    HoraAbertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    HoraFechamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GarcomId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    MesaId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Garcons_GarcomId",
                        column: x => x.GarcomId,
                        principalTable: "Garcons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Garcons_GarcomId1",
                        column: x => x.GarcomId1,
                        principalTable: "Garcons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Atendimentos_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Atendimentos_Mesas_MesaId1",
                        column: x => x.MesaId1,
                        principalTable: "Mesas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AtendimentoProdutos",
                columns: table => new
                {
                    AtendimentoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProdutoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtendimentoProdutos", x => new { x.AtendimentoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_AtendimentoProdutos_Atendimentos_AtendimentoId",
                        column: x => x.AtendimentoId,
                        principalTable: "Atendimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtendimentoProdutos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Opções de entrada para abrir o apetite", "Entradas" },
                    { 2, "Pratos principais com carnes, peixes e vegetarianos", "Pratos Principais" },
                    { 3, "Acompanhamentos para os pratos principais", "Acompanhamentos" },
                    { 4, "Sobremesas deliciosas para fechar a refeição", "Sobremesas" },
                    { 5, "Diversas opções de bebidas para acompanhar a refeição", "Bebidas" }
                });

            migrationBuilder.InsertData(
                table: "Garcons",
                columns: new[] { "Id", "Nome", "NumeroTelefone", "Sobrenome" },
                values: new object[,]
                {
                    { 1, "André", "45999999991", "Soares" },
                    { 2, "Fernanda", "45999999992", "Silva" },
                    { 3, "Henrique", "45999999993", "Souza" },
                    { 4, "Juliano", "45999999994", "Santos" },
                    { 5, "Sabrina", "45999999995", "Moreira" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "HoraAbertura", "Numero", "Status" },
                values: new object[,]
                {
                    { 1, null, 1, 1 },
                    { 2, null, 2, 1 },
                    { 3, null, 3, 1 },
                    { 4, null, 4, 1 },
                    { 5, null, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Atendimentos",
                columns: new[] { "Id", "GarcomId", "GarcomId1", "HoraAbertura", "HoraFechamento", "MesaId", "MesaId1" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5517), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5529), 1, null },
                    { 2, 2, null, new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5534), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5534), 2, null },
                    { 3, 3, null, new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5535), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5535), 3, null },
                    { 4, 4, null, new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5536), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5537), 4, null },
                    { 5, 5, null, new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5537), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5538), 5, null }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "Nome", "Preco" },
                values: new object[,]
                {
                    { 1, 1, "Carpaccio de carne com rúcula, parmesão e molho de mostarda", "Carpaccio de Carne", 25.90m },
                    { 2, 2, "Risoto com cogumelos frescos, parmesão e azeite trufado", "Risoto de Funghi", 39.90m },
                    { 3, 2, "Arroz com pato, linguiça, ervilhas e azeitonas", "Arroz de Pato", 44.90m },
                    { 4, 3, "Purê de batatas com alho e cebolinha", "Purê de Batatas", 9.90m },
                    { 5, 4, "Mousse de chocolate com chantilly e raspas de chocolate", "Mousse de Chocolate", 15.90m },
                    { 6, 5, "Vinho tinto chileno de uva cabernet sauvignon", "Vinho Tinto Chileno", 69.90m }
                });

            migrationBuilder.InsertData(
                table: "AtendimentoProdutos",
                columns: new[] { "AtendimentoId", "ProdutoId", "Preco", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 24.9m, 1 },
                    { 1, 2, 39.9m, 2 },
                    { 1, 3, 40m, 1 },
                    { 1, 4, 9.9m, 3 },
                    { 1, 5, 14.9m, 5 },
                    { 2, 1, 25.9m, 3 },
                    { 2, 2, 37.5m, 4 },
                    { 2, 3, 39.9m, 3 },
                    { 2, 4, 17.9m, 2 },
                    { 2, 5, 15.9m, 5 },
                    { 2, 6, 69.9m, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtendimentoProdutos_ProdutoId",
                table: "AtendimentoProdutos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_GarcomId",
                table: "Atendimentos",
                column: "GarcomId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_GarcomId1",
                table: "Atendimentos",
                column: "GarcomId1");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MesaId",
                table: "Atendimentos",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendimentos_MesaId1",
                table: "Atendimentos",
                column: "MesaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtendimentoProdutos");

            migrationBuilder.DropTable(
                name: "Atendimentos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Garcons");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
