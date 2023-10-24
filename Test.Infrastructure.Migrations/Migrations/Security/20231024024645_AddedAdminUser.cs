using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Infrastructure.Migrations.Migrations.Security
{
    /// <inheritdoc />
    public partial class AddedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "tenant_id",
                table: "user",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "created_at", "created_by", "email", "name", "password", "tenant_id", "updated_at", "updated_by" },
                values: new object[] { 1, new DateTime(2023, 10, 24, 2, 46, 45, 479, DateTimeKind.Utc).AddTicks(6298), 0, "admin@admin.com", "Admin", "AXsylzBoK0cEbxWE3TWzMA==.09d+cQlb2zsQEWgsiJgeNTDhZRJkA79Qeq20llsJIgg=", null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "tenant_id",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
