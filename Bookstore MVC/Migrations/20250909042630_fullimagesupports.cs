using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookstore_MVC.Migrations
{
    /// <inheritdoc />
    public partial class fullimagesupports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "Book",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Book",
                newName: "description");
        }
    }
}
