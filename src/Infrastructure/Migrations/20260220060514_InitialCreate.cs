using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "DaySchedule",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                GroupName = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                Date = table.Column<DateOnly>(type: "Date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DaySchedule", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Specialty",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specialty", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Teacher",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FullName = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Teacher", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Lesson",
            columns: table => new
            {
                Groupid = table.Column<Guid>(type: "uuid", nullable: false),
                Subject1 = table.Column<string>(type: "text", nullable: false),
                Subject2 = table.Column<string>(type: "text", nullable: false),
                Teacher1 = table.Column<string>(type: "text", nullable: false),
                Teacher2 = table.Column<string>(type: "text", nullable: false),
                Classroom1 = table.Column<string>(type: "text", nullable: false),
                Classroom2 = table.Column<string>(type: "text", nullable: false),
                StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Lesson", x => x.Groupid);
                table.ForeignKey(
                    name: "FK_Lesson_DaySchedule_Groupid",
                    column: x => x.Groupid,
                    principalTable: "DaySchedule",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Group",
            columns: table => new
            {
                SpecialtyId = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Group", x => x.SpecialtyId);
                table.ForeignKey(
                    name: "FK_Group_Specialty_SpecialtyId",
                    column: x => x.SpecialtyId,
                    principalTable: "Specialty",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "Date_Index",
            table: "DaySchedule",
            column: "Date");

        migrationBuilder.CreateIndex(
            name: "Group_Index",
            table: "DaySchedule",
            column: "GroupName");

        migrationBuilder.CreateIndex(
            name: "Teachers_Index",
            table: "Lesson",
            columns: new[] { "Teacher1", "Teacher2" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Group");

        migrationBuilder.DropTable(
            name: "Lesson");

        migrationBuilder.DropTable(
            name: "Teacher");

        migrationBuilder.DropTable(
            name: "Specialty");

        migrationBuilder.DropTable(
            name: "DaySchedule");
    }
}
