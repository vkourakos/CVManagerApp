using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVManagerapp.Data.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certifications_CVs_CVId",
                table: "Certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CVs_CVId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_CVs_CVId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_CVs_CVId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_CVs_CVId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_CVs_CVId",
                table: "WorkExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "WorkExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Languages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Interests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Educations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Certifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Certifications_CVs_CVId",
                table: "Certifications",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_CVs_CVId",
                table: "Educations",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_CVs_CVId",
                table: "Interests",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_CVs_CVId",
                table: "Languages",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_CVs_CVId",
                table: "Skills",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_CVs_CVId",
                table: "WorkExperiences",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certifications_CVs_CVId",
                table: "Certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_CVs_CVId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Interests_CVs_CVId",
                table: "Interests");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_CVs_CVId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_CVs_CVId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkExperiences_CVs_CVId",
                table: "WorkExperiences");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "WorkExperiences",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Skills",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Languages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Interests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Educations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CVId",
                table: "Certifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Certifications_CVs_CVId",
                table: "Certifications",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_CVs_CVId",
                table: "Educations",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_CVs_CVId",
                table: "Interests",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_CVs_CVId",
                table: "Languages",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CVs_CVId",
                table: "Projects",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_CVs_CVId",
                table: "Skills",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkExperiences_CVs_CVId",
                table: "WorkExperiences",
                column: "CVId",
                principalTable: "CVs",
                principalColumn: "Id");
        }
    }
}
