using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class EventFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecurringEventId1",
                table: "Events",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RecurringEventId1",
                table: "Events",
                column: "RecurringEventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_RecurringEventId1",
                table: "Events",
                column: "RecurringEventId1",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_RecurringEventId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RecurringEventId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RecurringEventId1",
                table: "Events");
        }
    }
}
