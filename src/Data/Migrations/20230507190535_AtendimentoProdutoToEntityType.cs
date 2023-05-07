using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AtendimentoProdutoToEntityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AtendimentoProdutos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 1, 2 },
                column: "Id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 1, 3 },
                column: "Id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 1, 4 },
                column: "Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 1, 5 },
                column: "Id",
                value: 5);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 1 },
                column: "Id",
                value: 6);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 2 },
                column: "Id",
                value: 7);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 3 },
                column: "Id",
                value: 8);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 4 },
                column: "Id",
                value: 9);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 5 },
                column: "Id",
                value: 10);

            migrationBuilder.UpdateData(
                table: "AtendimentoProdutos",
                keyColumns: new[] { "AtendimentoId", "ProdutoId" },
                keyValues: new object[] { 2, 6 },
                column: "Id",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 7, 16, 5, 35, 618, DateTimeKind.Local).AddTicks(2983), new DateTime(2023, 5, 7, 17, 5, 35, 618, DateTimeKind.Local).AddTicks(2998) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 7, 16, 5, 35, 618, DateTimeKind.Local).AddTicks(3006), new DateTime(2023, 5, 7, 17, 5, 35, 618, DateTimeKind.Local).AddTicks(3007) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 7, 16, 5, 35, 618, DateTimeKind.Local).AddTicks(3008), new DateTime(2023, 5, 7, 17, 5, 35, 618, DateTimeKind.Local).AddTicks(3009) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 7, 16, 5, 35, 618, DateTimeKind.Local).AddTicks(3010), new DateTime(2023, 5, 7, 17, 5, 35, 618, DateTimeKind.Local).AddTicks(3011) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 7, 16, 5, 35, 618, DateTimeKind.Local).AddTicks(3012), new DateTime(2023, 5, 7, 17, 5, 35, 618, DateTimeKind.Local).AddTicks(3013) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AtendimentoProdutos");

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5517), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5529) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5534), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5534) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5535), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5535) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5536), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5537) });

            migrationBuilder.UpdateData(
                table: "Atendimentos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HoraAbertura", "HoraFechamento" },
                values: new object[] { new DateTime(2023, 5, 6, 22, 1, 0, 481, DateTimeKind.Local).AddTicks(5537), new DateTime(2023, 5, 6, 23, 1, 0, 481, DateTimeKind.Local).AddTicks(5538) });
        }
    }
}
