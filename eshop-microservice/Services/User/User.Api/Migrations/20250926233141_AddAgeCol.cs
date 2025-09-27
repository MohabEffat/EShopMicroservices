using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "AspNetUsers");
        }
    }
}
