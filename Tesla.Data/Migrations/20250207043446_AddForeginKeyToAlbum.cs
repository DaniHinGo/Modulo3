using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tesla.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeginKeyToAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Album",
                newName: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Album",
                newName: "Gender");
        }
    }
}
