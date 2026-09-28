using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventra.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "SalesOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveredByDriverId",
                table: "SalesOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_DeliveredByDriverId",
                table: "SalesOrders",
                column: "DeliveredByDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrders_Employees_DeliveredByDriverId",
                table: "SalesOrders",
                column: "DeliveredByDriverId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrders_Employees_DeliveredByDriverId",
                table: "SalesOrders");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrders_DeliveredByDriverId",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "DeliveredByDriverId",
                table: "SalesOrders");
        }
    }
}
