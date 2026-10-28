using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Albums.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "albumnes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Artista = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AnioLanzamiento = table.Column<int>(type: "int", nullable: false),
                    CantidadCanciones = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albumnes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "albumnes",
                columns: new[] { "Id", "AnioLanzamiento", "Artista", "CantidadCanciones", "Genero", "Titulo" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1982, "Michael Jackson", 9, "Pop", "Thriller" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1969, "The Beatles", 17, "Rock", "Abbey Road" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 2022, "Bad Bunny", 23, "Reggaetón", "Un Verano Sin Ti" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "albumnes");
        }
    }
}
