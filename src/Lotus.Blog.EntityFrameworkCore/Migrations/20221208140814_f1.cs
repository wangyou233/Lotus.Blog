using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotus.Blog.EntityFrameworkCore.Migrations
{
    public partial class f1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Abstract",
                table: "blog_Posts",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "blog_TermNodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Created" },
                values: new object[] { "12/8/2022 10:08:13 PM", new DateTime(2022, 12, 8, 22, 8, 13, 671, DateTimeKind.Local).AddTicks(8030) });

            migrationBuilder.UpdateData(
                table: "blog_admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 8, 22, 8, 13, 671, DateTimeKind.Local).AddTicks(8216));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abstract",
                table: "blog_Posts");

            migrationBuilder.UpdateData(
                table: "blog_TermNodes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "Created" },
                values: new object[] { "12/8/2022 7:07:22 PM", new DateTime(2022, 12, 8, 19, 7, 22, 824, DateTimeKind.Local).AddTicks(9199) });

            migrationBuilder.UpdateData(
                table: "blog_admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2022, 12, 8, 19, 7, 22, 824, DateTimeKind.Local).AddTicks(9379));
        }
    }
}
