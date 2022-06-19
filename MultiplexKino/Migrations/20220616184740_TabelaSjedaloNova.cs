using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class TabelaSjedaloNova : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sala_Sjedala_SjedalaId",
                table: "Sala");

            migrationBuilder.DropTable(
                name: "Sjedala");

            migrationBuilder.DropIndex(
                name: "IX_Sala_SjedalaId",
                table: "Sala");

            migrationBuilder.DropColumn(
                name: "SjedalaId",
                table: "Sala");

            migrationBuilder.CreateTable(
                name: "Sjedalo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSjedala = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsZauzeto = table.Column<bool>(type: "bit", nullable: false),
                    SalaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sjedalo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sjedalo_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sjedalo_SalaId",
                table: "Sjedalo",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sjedalo");

            migrationBuilder.AddColumn<int>(
                name: "SjedalaId",
                table: "Sala",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Sjedala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSjedala = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsZauzeto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sjedala", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sala_SjedalaId",
                table: "Sala",
                column: "SjedalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sala_Sjedala_SjedalaId",
                table: "Sala",
                column: "SjedalaId",
                principalTable: "Sjedala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
