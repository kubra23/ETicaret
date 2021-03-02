using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicaret.DAL.Migrations
{
    public partial class mig_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "e4eaeb25-4220-45f4-870a-8fbb96afd776");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "0f5cd8cd-915f-4d0a-a70b-99bf27bda6b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e12e9001-dc01-465a-a059-fb80a097d509");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b1535456-b7e2-41d2-a72c-5ab42e355aa8", "AQAAAAEAACcQAAAAEKZxIRuNINgwG5eERyUindsxSmgOT0oNXGoKiVKiCLqvX0a/6AEZp7038URuRciLDg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
