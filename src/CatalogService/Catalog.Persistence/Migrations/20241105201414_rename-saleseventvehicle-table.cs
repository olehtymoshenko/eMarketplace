using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renamesaleseventvehicletable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sale_event_vehicle_sale_event_sale_event_id",
                table: "sale_event_vehicle");

            migrationBuilder.DropForeignKey(
                name: "fk_sale_event_vehicle_vehicles_vehicle_id",
                table: "sale_event_vehicle");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sale_event_vehicle",
                table: "sale_event_vehicle");

            migrationBuilder.RenameTable(
                name: "sale_event_vehicle",
                newName: "sale_event_vehicles");

            migrationBuilder.RenameIndex(
                name: "ix_sale_event_vehicle_vehicle_id",
                table: "sale_event_vehicles",
                newName: "ix_sale_event_vehicles_vehicle_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sale_event_vehicles",
                table: "sale_event_vehicles",
                columns: new[] { "sale_event_id", "vehicle_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_sale_event_vehicles_sale_events_sale_event_id",
                table: "sale_event_vehicles",
                column: "sale_event_id",
                principalTable: "sale_events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sale_event_vehicles_vehicles_vehicle_id",
                table: "sale_event_vehicles",
                column: "vehicle_id",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_sale_event_vehicles_sale_events_sale_event_id",
                table: "sale_event_vehicles");

            migrationBuilder.DropForeignKey(
                name: "fk_sale_event_vehicles_vehicles_vehicle_id",
                table: "sale_event_vehicles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sale_event_vehicles",
                table: "sale_event_vehicles");

            migrationBuilder.RenameTable(
                name: "sale_event_vehicles",
                newName: "sale_event_vehicle");

            migrationBuilder.RenameIndex(
                name: "ix_sale_event_vehicles_vehicle_id",
                table: "sale_event_vehicle",
                newName: "ix_sale_event_vehicle_vehicle_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sale_event_vehicle",
                table: "sale_event_vehicle",
                columns: new[] { "sale_event_id", "vehicle_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_sale_event_vehicle_sale_event_sale_event_id",
                table: "sale_event_vehicle",
                column: "sale_event_id",
                principalTable: "sale_events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sale_event_vehicle_vehicles_vehicle_id",
                table: "sale_event_vehicle",
                column: "vehicle_id",
                principalTable: "vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
