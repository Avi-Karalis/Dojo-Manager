using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentMembershipFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPaidThisMonth",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisitor",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPaidThisMonth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsVisitor",
                table: "Students");
        }
    }
}
