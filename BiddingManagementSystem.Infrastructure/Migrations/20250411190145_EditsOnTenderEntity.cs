using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditsOnTenderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deliverables",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "EligibilityCriteria",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "ScopeOfWork",
                table: "Tenders");

            migrationBuilder.DropColumn(
                name: "SubmissionGuidelines",
                table: "Tenders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deliverables",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EligibilityCriteria",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScopeOfWork",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubmissionGuidelines",
                table: "Tenders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
