using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Cloud.Games.Core.Infra.Repositories.EF.Migrations
{
    /// <inheritdoc />
    public partial class add_type_player : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Player");
        }
    }
}
