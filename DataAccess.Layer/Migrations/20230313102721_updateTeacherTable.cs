using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.AccessLayer.Migrations
{
    public partial class updateTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "MotherName",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Teacher",
                newName: "Designation");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Teacher",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Teacher",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Teacher");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "Teacher",
                newName: "Nationality");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Teacher",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                table: "Teacher",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
