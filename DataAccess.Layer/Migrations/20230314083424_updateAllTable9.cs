using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.AccessLayer.Migrations
{
    public partial class updateAllTable9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClassInformation_TeacherID",
                table: "ClassInformation");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInformation_TeacherID",
                table: "ClassInformation",
                column: "TeacherID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ClassInformation_TeacherID",
                table: "ClassInformation");

            migrationBuilder.CreateIndex(
                name: "IX_ClassInformation_TeacherID",
                table: "ClassInformation",
                column: "TeacherID",
                unique: true);
        }
    }
}
