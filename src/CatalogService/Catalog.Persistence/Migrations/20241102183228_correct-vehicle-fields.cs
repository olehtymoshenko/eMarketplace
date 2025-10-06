#pragma warning disable 8981
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correctvehiclefields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vehicles_clients_vehicle_id",
                table: "vehicles");

            migrationBuilder.RenameColumn(
                name: "vehicle_id",
                table: "vehicles",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "ix_vehicles_vehicle_id",
                table: "vehicles",
                newName: "ix_vehicles_client_id");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                table: "clients",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddForeignKey(
                name: "fk_vehicles_clients_client_id",
                table: "vehicles",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vehicles_clients_client_id",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "xmin",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "vehicles",
                newName: "vehicle_id");

            migrationBuilder.RenameIndex(
                name: "ix_vehicles_client_id",
                table: "vehicles",
                newName: "ix_vehicles_vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "fk_vehicles_clients_vehicle_id",
                table: "vehicles",
                column: "vehicle_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
