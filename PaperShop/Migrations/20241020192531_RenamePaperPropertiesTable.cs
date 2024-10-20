using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperShop.Migrations
{
    /// <inheritdoc />
    public partial class RenamePaperPropertiesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PaperProperties",
                table: "PaperProperties");

            migrationBuilder.RenameTable(
                name: "PaperProperties",
                newName: "paper_properties");

            migrationBuilder.RenameIndex(
                name: "IX_PaperProperties_PropertyId",
                table: "paper_properties",
                newName: "IX_paper_properties_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_paper_properties",
                table: "paper_properties",
                columns: new[] { "PaperId", "PropertyId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_paper_properties",
                table: "paper_properties");

            migrationBuilder.RenameTable(
                name: "paper_properties",
                newName: "PaperProperties");

            migrationBuilder.RenameIndex(
                name: "IX_paper_properties_PropertyId",
                table: "PaperProperties",
                newName: "IX_PaperProperties_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaperProperties",
                table: "PaperProperties",
                columns: new[] { "PaperId", "PropertyId" });
        }
    }
}
