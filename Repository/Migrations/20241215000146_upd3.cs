using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class upd3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TitleTypeId",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TitleTypes",
                columns: table => new
                {
                    TitleTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitleTypes", x => x.TitleTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_TitleTypeId",
                table: "Films",
                column: "TitleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_TitleTypes_TitleTypeId",
                table: "Films",
                column: "TitleTypeId",
                principalTable: "TitleTypes",
                principalColumn: "TitleTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_TitleTypes_TitleTypeId",
                table: "Films");

            migrationBuilder.DropTable(
                name: "TitleTypes");

            migrationBuilder.DropIndex(
                name: "IX_Films_TitleTypeId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "TitleTypeId",
                table: "Films");
        }
    }
}
