using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ReleasedYear = table.Column<int>(type: "integer", nullable: false),
                    Certificate = table.Column<string>(type: "text", nullable: true),
                    Runtime = table.Column<int>(type: "integer", nullable: false),
                    ImdbRating = table.Column<double>(type: "double precision", nullable: false),
                    ShortStory = table.Column<string>(type: "text", nullable: true),
                    MetaScore = table.Column<int>(type: "integer", nullable: false),
                    NumberOfVotes = table.Column<int>(type: "integer", nullable: false),
                    Gross = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.FilmId);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    DirectorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.DirectorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    GoogleId = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ActorFilm",
                columns: table => new
                {
                    ActorsActorId = table.Column<int>(type: "integer", nullable: false),
                    FilmsFilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorFilm", x => new { x.ActorsActorId, x.FilmsFilmId });
                    table.ForeignKey(
                        name: "FK_ActorFilm_Actors_ActorsActorId",
                        column: x => x.ActorsActorId,
                        principalTable: "Actors",
                        principalColumn: "ActorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorFilm_Clients_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmLinks",
                columns: table => new
                {
                    FilmLinkId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Language = table.Column<string>(type: "text", nullable: false),
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmLinks", x => x.FilmLinkId);
                    table.ForeignKey(
                        name: "FK_FilmLinks_Clients_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectorFilm",
                columns: table => new
                {
                    DirectorsDirectorId = table.Column<int>(type: "integer", nullable: false),
                    FilmsFilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorFilm", x => new { x.DirectorsDirectorId, x.FilmsFilmId });
                    table.ForeignKey(
                        name: "FK_DirectorFilm_Clients_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorFilm_Directors_DirectorsDirectorId",
                        column: x => x.DirectorsDirectorId,
                        principalTable: "Directors",
                        principalColumn: "DirectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenre",
                columns: table => new
                {
                    FilmsFilmId = table.Column<int>(type: "integer", nullable: false),
                    GenresGenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenre", x => new { x.FilmsFilmId, x.GenresGenreId });
                    table.ForeignKey(
                        name: "FK_FilmGenre_Clients_FilmsFilmId",
                        column: x => x.FilmsFilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenre_Genres_GenresGenreId",
                        column: x => x.GenresGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmPoints",
                columns: table => new
                {
                    FilmPointId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Point = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmPoints", x => x.FilmPointId);
                    table.ForeignKey(
                        name: "FK_FilmPoints_Clients_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Overviews",
                columns: table => new
                {
                    OverviewId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserOverview = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Overviews", x => x.OverviewId);
                    table.ForeignKey(
                        name: "FK_Overviews_Clients_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Overviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewStatuses",
                columns: table => new
                {
                    ViewStatusId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FilmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewStatuses", x => x.ViewStatusId);
                    table.ForeignKey(
                        name: "FK_ViewStatuses_Clients_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Clients",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorFilm_FilmsFilmId",
                table: "ActorFilm",
                column: "FilmsFilmId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorFilm_FilmsFilmId",
                table: "DirectorFilm",
                column: "FilmsFilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_GenresGenreId",
                table: "FilmGenre",
                column: "GenresGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmLinks_FilmId",
                table: "FilmLinks",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmPoints_FilmId",
                table: "FilmPoints",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmPoints_UserId",
                table: "FilmPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Overviews_FilmId",
                table: "Overviews",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Overviews_UserId",
                table: "Overviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewStatuses_FilmId",
                table: "ViewStatuses",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewStatuses_UserId",
                table: "ViewStatuses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorFilm");

            migrationBuilder.DropTable(
                name: "DirectorFilm");

            migrationBuilder.DropTable(
                name: "FilmGenre");

            migrationBuilder.DropTable(
                name: "FilmLinks");

            migrationBuilder.DropTable(
                name: "FilmPoints");

            migrationBuilder.DropTable(
                name: "Overviews");

            migrationBuilder.DropTable(
                name: "ViewStatuses");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
