using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LesKita.Migrations
{
    /// <inheritdoc />
    public partial class dbUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deskripsi",
                table: "T2Jadwal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deskripsi",
                table: "T2Jadwal");
        }
    }
}
