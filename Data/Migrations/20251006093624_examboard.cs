using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CertiPurge.Migrations
{
    /// <inheritdoc />
    public partial class examboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateIssued",
                table: "Certificates",
                newName: "AwardingDate");

            migrationBuilder.AddColumn<string>(
                name: "ExamBoard",
                table: "Certificates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamBoard",
                table: "Certificates");

            migrationBuilder.RenameColumn(
                name: "AwardingDate",
                table: "Certificates",
                newName: "DateIssued");
        }
    }
}
