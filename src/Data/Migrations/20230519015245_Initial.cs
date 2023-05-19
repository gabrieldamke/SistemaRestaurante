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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garcons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HoraAbertura = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MesaId = table.Column<int>(type: "int", nullable: false),
                    GarcomId = table.Column<int>(type: "int", nullable: false),
                    HoraAbertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFechamento = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        name: "FK_Atendimentos_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtendimentoProdutos",
                columns: table => new
                {
                    AtendimentoId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "Id", "GarcomId", "HoraAbertura", "HoraFechamento", "MesaId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 5, 18, 22, 52, 45, 339, DateTimeKind.Local).AddTicks(8752), new DateTime(2023, 5, 18, 23, 52, 45, 339, DateTimeKind.Local).AddTicks(8763), 1 },
                    { 2, 2, new DateTime(2023, 5, 18, 22, 52, 45, 339, DateTimeKind.Local).AddTicks(8769), new DateTime(2023, 5, 18, 23, 52, 45, 339, DateTimeKind.Local).AddTicks(8770), 2 },
                    { 3, 3, new DateTime(2023, 5, 18, 22, 52, 45, 339, DateTimeKind.Local).AddTicks(8771), new DateTime(2023, 5, 18, 23, 52, 45, 339, DateTimeKind.Local).AddTicks(8771), 3 },
                    { 4, 4, new DateTime(2023, 5, 18, 22, 52, 45, 339, DateTimeKind.Local).AddTicks(8772), new DateTime(2023, 5, 18, 23, 52, 45, 339, DateTimeKind.Local).AddTicks(8772), 4 },
                    { 5, 5, new DateTime(2023, 5, 18, 22, 52, 45, 339, DateTimeKind.Local).AddTicks(8773), new DateTime(2023, 5, 18, 23, 52, 45, 339, DateTimeKind.Local).AddTicks(8773), 5 }
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
                columns: new[] { "AtendimentoId", "ProdutoId", "Id", "Preco", "Quantidade" },
                values: new object[,]
                {
                    { 1, 1, 1, 24.9m, 1 },
                    { 1, 2, 2, 39.9m, 2 },
                    { 1, 3, 3, 40m, 1 },
                    { 1, 4, 4, 9.9m, 3 },
                    { 1, 5, 5, 14.9m, 5 },
                    { 2, 1, 6, 25.9m, 3 },
                    { 2, 2, 7, 37.5m, 4 },
                    { 2, 3, 8, 39.9m, 3 },
                    { 2, 4, 9, 17.9m, 2 },
                    { 2, 5, 10, 15.9m, 5 },
                    { 2, 6, 11, 69.9m, 1 }
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
                name: "IX_Atendimentos_MesaId",
                table: "Atendimentos",
                column: "MesaId");

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
