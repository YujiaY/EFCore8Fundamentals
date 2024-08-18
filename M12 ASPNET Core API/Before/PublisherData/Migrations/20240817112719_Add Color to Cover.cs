using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublisherData.Migrations
{
    /// <inheritdoc />
    public partial class AddColortoCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrimaryColor",
                table: "Covers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 1,
                column: "PrimaryColor",
                value: "Color [Aqua]");

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 2,
                column: "PrimaryColor",
                value: "Color [Red]");

            migrationBuilder.UpdateData(
                table: "Covers",
                keyColumn: "CoverId",
                keyValue: 3,
                column: "PrimaryColor",
                value: "Color [Brown]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryColor",
                table: "Covers");
        }
    }
}
