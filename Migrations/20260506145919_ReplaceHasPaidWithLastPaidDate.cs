using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DojoManager.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceHasPaidWithLastPaidDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPaidThisMonth",
                table: "Students");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaidDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPaidDate",
                table: "Students");

            migrationBuilder.AddColumn<bool>(
                name: "HasPaidThisMonth",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
