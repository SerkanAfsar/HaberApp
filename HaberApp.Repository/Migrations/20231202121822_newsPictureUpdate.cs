using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaberApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class newsPictureUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsPicture",
                table: "News",
                newName: "NewsPictureSmall");

            migrationBuilder.AddColumn<string>(
                name: "NewsPictureBig",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsPictureMedium",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsPictureBig",
                table: "News");

            migrationBuilder.DropColumn(
                name: "NewsPictureMedium",
                table: "News");

            migrationBuilder.RenameColumn(
                name: "NewsPictureSmall",
                table: "News",
                newName: "NewsPicture");
        }
    }
}
