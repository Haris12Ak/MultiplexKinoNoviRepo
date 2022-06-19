using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class DeleteTabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sjedala");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sjedala",
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
                    table.PrimaryKey("PK_Sjedala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sjedala_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sjedala_SalaId",
                table: "Sjedala",
                column: "SalaId");
        }
    }
}
