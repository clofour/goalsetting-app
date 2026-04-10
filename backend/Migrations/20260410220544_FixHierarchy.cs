using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_BearingId",
                table: "Goals");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_NorthStarId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_BearingId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_NorthStarId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "BearingId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "NorthStarId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Events",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "ID");

            migrationBuilder.AddColumn<Guid>(
                name: "BearingId",
                table: "Goals",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NorthStarId",
                table: "Goals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_BearingId",
                table: "Goals",
                column: "BearingId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_NorthStarId",
                table: "Goals",
                column: "NorthStarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_BearingId",
                table: "Goals",
                column: "BearingId",
                principalTable: "Goals",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_NorthStarId",
                table: "Goals",
                column: "NorthStarId",
                principalTable: "Goals",
                principalColumn: "Id");
        }
    }
}
