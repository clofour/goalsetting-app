using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class goalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attainable",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "backupPlan",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "measurable",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "realistic",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "specific",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "timeBound",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "why",
                table: "Goals");

            migrationBuilder.AlterColumn<string>(
                name: "weaknesses",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "strengths",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parentID",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "obstacles",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "motivationType",
                table: "Goals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "motivation",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "killConditions",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "importance",
                table: "Goals",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "Goals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Movement_parentID",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NorthStar_description",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NorthStar_justification",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "justification",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "opts",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "temptations",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "triggers",
                table: "Goals",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goals_Movement_parentID",
                table: "Goals",
                column: "Movement_parentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Goals_Movement_parentID",
                table: "Goals",
                column: "Movement_parentID",
                principalTable: "Goals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Goals_Movement_parentID",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Goals_Movement_parentID",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Movement_parentID",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "NorthStar_description",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "NorthStar_justification",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "justification",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "opts",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "temptations",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "triggers",
                table: "Goals");

            migrationBuilder.AlterColumn<string>(
                name: "weaknesses",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "strengths",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "parentID",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "obstacles",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "motivationType",
                table: "Goals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "motivation",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "killConditions",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "importance",
                table: "Goals",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "attainable",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "backupPlan",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "difficulty",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "measurable",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "realistic",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "specific",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "timeBound",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "why",
                table: "Goals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
