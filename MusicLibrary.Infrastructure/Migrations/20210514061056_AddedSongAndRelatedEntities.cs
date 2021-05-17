using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicLibrary.Infrastructure.Migrations
{
    public partial class AddedSongAndRelatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "AlbumComposers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "AlbumComposers");
        }
    }
}
