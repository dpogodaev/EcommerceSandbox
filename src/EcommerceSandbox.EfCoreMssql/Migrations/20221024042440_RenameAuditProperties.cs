using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceSandbox.EfCoreMssql.Migrations
{
    public partial class RenameAuditProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Products",
                newName: "LastModificationTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Products",
                newName: "CreationTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModificationTime",
                table: "Products",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "Products",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Products",
                type: "datetime2",
                nullable: true);
        }
    }
}
