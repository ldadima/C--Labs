using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Shop.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "ShopLibrary",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capacity = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopLibrary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Genre = table.Column<string>(maxLength: 100, nullable: false),
                    IsNew = table.Column<bool>(nullable: false),
                    DateDelivery = table.Column<DateTime>(nullable: false),
                    ShopId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_ShopLibrary_ShopId",
                        column: x => x.ShopId,
                        principalSchema: "public",
                        principalTable: "ShopLibrary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "ShopLibrary",
                columns: new[] { "Id", "Balance", "Capacity" },
                values: new object[] { 1L, 100000.0, 500 });

            migrationBuilder.CreateIndex(
                name: "IX_Book_ShopId",
                schema: "public",
                table: "Book",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ShopLibrary",
                schema: "public");
        }
    }
}
