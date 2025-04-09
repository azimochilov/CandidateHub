using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateHub.Migrations
{
    public partial class RemoveIsDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Candidates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Candidates",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
