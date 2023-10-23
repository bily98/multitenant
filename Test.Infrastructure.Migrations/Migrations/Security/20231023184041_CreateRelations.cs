using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Infrastructure.Migrations.Migrations.Security
{
    /// <inheritdoc />
    public partial class CreateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_user_tenant_id",
                table: "user",
                column: "tenant_id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_tenant_tenant_id",
                table: "user",
                column: "tenant_id",
                principalTable: "tenant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_tenant_tenant_id",
                table: "user");

            migrationBuilder.DropIndex(
                name: "ix_user_tenant_id",
                table: "user");
        }
    }
}
