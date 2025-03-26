using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDBA.Migrations
{
    /// <inheritdoc />
    public partial class EnsureAppDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Events",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Events",
                newName: "EventName");
        }
    }
}
