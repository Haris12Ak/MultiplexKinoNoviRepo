using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class AddedNewTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    BrojSjedista = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Trajanje = table.Column<int>(type: "int", nullable: true),
                    GodinaSnimanja = table.Column<int>(type: "int", nullable: true),
                    Reditelj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Glumac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sadrzaj = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    VideoLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Plakat = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ZanrId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Film_Zanr_ZanrId",
                        column: x => x.ZanrId,
                        principalTable: "Zanr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekcija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijediOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VrijediDo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekcija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projekcija_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projekcija_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_ZanrId",
                table: "Film",
                column: "ZanrId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcija_FilmId",
                table: "Projekcija",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Projekcija_SalaId",
                table: "Projekcija",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projekcija");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Zanr");
        }
    }
}
