using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class finalversionofclientandvehicleentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "propeprties",
                table: "vehicles",
                newName: "properties");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "vehicles",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "xmin",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "properties",
                table: "vehicles",
                newName: "propeprties");
        }
    }
}
