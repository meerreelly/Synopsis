using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilm_Clients_FilmsFilmId",
                table: "ActorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorFilm_Clients_FilmsFilmId",
                table: "DirectorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Clients_FilmsFilmId",
                table: "FilmGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmLinks_Clients_FilmId",
                table: "FilmLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmPoints_Clients_FilmId",
                table: "FilmPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Overviews_Clients_FilmId",
                table: "Overviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewStatuses_Clients_FilmId",
                table: "ViewStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Films");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Films",
                table: "Films",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilm_Films_FilmsFilmId",
                table: "ActorFilm",
                column: "FilmsFilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorFilm_Films_FilmsFilmId",
                table: "DirectorFilm",
                column: "FilmsFilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Films_FilmsFilmId",
                table: "FilmGenre",
                column: "FilmsFilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmLinks_Films_FilmId",
                table: "FilmLinks",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmPoints_Films_FilmId",
                table: "FilmPoints",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Overviews_Films_FilmId",
                table: "Overviews",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewStatuses_Films_FilmId",
                table: "ViewStatuses",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActorFilm_Films_FilmsFilmId",
                table: "ActorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectorFilm_Films_FilmsFilmId",
                table: "DirectorFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Films_FilmsFilmId",
                table: "FilmGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmLinks_Films_FilmId",
                table: "FilmLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmPoints_Films_FilmId",
                table: "FilmPoints");

            migrationBuilder.DropForeignKey(
                name: "FK_Overviews_Films_FilmId",
                table: "Overviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewStatuses_Films_FilmId",
                table: "ViewStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Films",
                table: "Films");

            migrationBuilder.RenameTable(
                name: "Films",
                newName: "Clients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActorFilm_Clients_FilmsFilmId",
                table: "ActorFilm",
                column: "FilmsFilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectorFilm_Clients_FilmsFilmId",
                table: "DirectorFilm",
                column: "FilmsFilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Clients_FilmsFilmId",
                table: "FilmGenre",
                column: "FilmsFilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmLinks_Clients_FilmId",
                table: "FilmLinks",
                column: "FilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmPoints_Clients_FilmId",
                table: "FilmPoints",
                column: "FilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Overviews_Clients_FilmId",
                table: "Overviews",
                column: "FilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewStatuses_Clients_FilmId",
                table: "ViewStatuses",
                column: "FilmId",
                principalTable: "Clients",
                principalColumn: "FilmId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
