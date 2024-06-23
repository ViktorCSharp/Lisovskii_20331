using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lisovskii_20331.UI.Migrations
{
    /// <inheritdoc />
    public partial class Avatar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "AspNetUsers");
        }
    }
}
