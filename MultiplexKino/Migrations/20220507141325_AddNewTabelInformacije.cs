using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class AddNewTabelInformacije : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Informacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kreiran = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informacije", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informacije");
        }
    }
}
