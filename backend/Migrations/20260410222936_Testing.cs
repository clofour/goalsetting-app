using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_Movement_ParentId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_Movement_ParentId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Movement_ParentId",
                table: "Goals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Movement_ParentId",
                table: "Goals",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Movement_ParentId",
                table: "Goals",
                column: "Movement_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_Movement_ParentId",
                table: "Goals",
                column: "Movement_ParentId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
