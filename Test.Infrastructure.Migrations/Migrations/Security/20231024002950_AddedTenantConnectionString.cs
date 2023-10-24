using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Infrastructure.Migrations.Migrations.Security
{
    /// <inheritdoc />
    public partial class AddedTenantConnectionString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "connection_string",
                table: "tenant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "connection_string",
                table: "tenant");
        }
    }
}
