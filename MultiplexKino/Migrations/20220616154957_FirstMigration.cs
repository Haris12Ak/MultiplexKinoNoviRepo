using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projekcija_Rezervacija_RezervacijaId",
                table: "Projekcija");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropIndex(
                name: "IX_Projekcija_RezervacijaId",
                table: "Projekcija");

            migrationBuilder.DropColumn(
                name: "RezervacijaId",
                table: "Projekcija");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RezervacijaId",
                table: "Projekcija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsZauzet = table.Column<bool>(type: "bit", nullable: false),
                    NazivSjedala = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projekcija_RezervacijaId",
                table: "Projekcija",
                column: "RezervacijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projekcija_Rezervacija_RezervacijaId",
                table: "Projekcija",
                column: "RezervacijaId",
                principalTable: "Rezervacija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
