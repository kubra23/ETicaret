using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicaret.DAL.Migrations
{
    public partial class mig_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "eadfa845-63b6-4630-a8a9-780448a0aa6c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c8a864c7-0fd9-4fb6-a0ed-041edcc57e2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ee937755-0b17-47ab-97a2-068927a7dda9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1a7b98e0-4a50-4b6d-abce-01f8b2aa94a2", "AQAAAAEAACcQAAAAEMOTQnEPve1K9akQ4r596rCRVP2KI36hF88mLhwGefR/kJTu1furPWzR8XHNNAAsKw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
