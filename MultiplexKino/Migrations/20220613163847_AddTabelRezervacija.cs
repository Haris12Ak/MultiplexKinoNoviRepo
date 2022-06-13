using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiplexKino.Migrations
{
    public partial class AddTabelRezervacija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeatsForHall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatsForHall", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CinemaHall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatsForHallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaHall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CinemaHall_SeatsForHall_SeatsForHallId",
                        column: x => x.SeatsForHallId,
                        principalTable: "SeatsForHall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBusy = table.Column<bool>(type: "bit", nullable: false),
                    SeatsForHallId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_SeatsForHall_SeatsForHallId",
                        column: x => x.SeatsForHallId,
                        principalTable: "SeatsForHall",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CinemaHallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_CinemaHall_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalTable: "CinemaHall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviesForShowing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CinemaHallId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesForShowing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviesForShowing_CinemaHall_CinemaHallId",
                        column: x => x.CinemaHallId,
                        principalTable: "CinemaHall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoviesForShowing_Projekcija_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Projekcija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CinemaHallId",
                table: "Category",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_CinemaHall_SeatsForHallId",
                table: "CinemaHall",
                column: "SeatsForHallId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesForShowing_CinemaHallId",
                table: "MoviesForShowing",
                column: "CinemaHallId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesForShowing_MovieId",
                table: "MoviesForShowing",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SeatsForHallId",
                table: "Seats",
                column: "SeatsForHallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "MoviesForShowing");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "CinemaHall");

            migrationBuilder.DropTable(
                name: "SeatsForHall");
        }
    }
}
