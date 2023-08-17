using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutnorteApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaceName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GamesPlayed = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalsFor = table.Column<int>(type: "INTEGER", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamWins = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamDraws = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamLosses = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoundName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.RoundId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    TeamGroup = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    TeamColor = table.Column<string>(type: "TEXT", nullable: true),
                    TeamManager = table.Column<string>(type: "TEXT", nullable: true),
                    TeamPhoneNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatchDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    RoundId = table.Column<int>(type: "INTEGER", nullable: true),
                    HomeTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlaceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Ronda = table.Column<int>(type: "INTEGER", nullable: true),
                    Local = table.Column<int>(type: "INTEGER", nullable: true),
                    Visitante = table.Column<int>(type: "INTEGER", nullable: true),
                    Cancha = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                    table.ForeignKey(
                        name: "FK_Matches_Places_Cancha",
                        column: x => x.Cancha,
                        principalTable: "Places",
                        principalColumn: "PlaceId");
                    table.ForeignKey(
                        name: "FK_Matches_Rounds_Ronda",
                        column: x => x.Ronda,
                        principalTable: "Rounds",
                        principalColumn: "RoundId");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Local",
                        column: x => x.Local,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                    table.ForeignKey(
                        name: "FK_Matches_Teams_Visitante",
                        column: x => x.Visitante,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ResultDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    HomeScore = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayScore = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundId = table.Column<int>(type: "INTEGER", nullable: false),
                    HomeTeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    AwayTeamId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "RoundId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Cancha",
                table: "Matches",
                column: "Cancha");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Local",
                table: "Matches",
                column: "Local");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Ronda",
                table: "Matches",
                column: "Ronda");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Visitante",
                table: "Matches",
                column: "Visitante");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AwayTeamId",
                table: "Results",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_HomeTeamId",
                table: "Results",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_RoundId",
                table: "Results",
                column: "RoundId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Rounds");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
