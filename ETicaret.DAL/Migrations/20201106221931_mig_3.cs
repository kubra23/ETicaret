using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicaret.DAL.Migrations
{
    public partial class mig_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5b8787b5-1ac0-4385-b546-95900ed002bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cdb50624-073b-41c7-b3fc-4e467d1458cb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "4249ac25-ad9d-4ebf-b91d-7279cc7c0639");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ed4e5ab7-f9ed-4c8b-961e-8d48a7db0777", "AQAAAAEAACcQAAAAEKdDANbejDq+XtXshvPOD+QxZe7gK86cp8DdDXCmH2h02V/fguORiF2W1jgSfw1ZiQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4a3cdf7d-d7b5-4748-8c7f-9893d5d77cf4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "37ff537d-5902-4586-a108-196e33b34c91");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "eeafd047-abe3-4475-85cb-781dd4536b9e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "89c5e3cc-b5e3-4e97-b578-7188c8005591", "AQAAAAEAACcQAAAAEDYKCVIec6BQ/e7TFG6/BKfnqyKFgveCuffd5OOTR6zBM4Hefh54Sdaf9+HEKbt79Q==" });
        }
    }
}
