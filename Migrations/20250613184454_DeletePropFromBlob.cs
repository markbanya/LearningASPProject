using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningProjectASP.Migrations
{
    /// <inheritdoc />
    public partial class DeletePropFromBlob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetadataJson",
                table: "Blobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetadataJson",
                table: "Blobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
