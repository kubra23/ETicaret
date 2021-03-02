using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicaret.DAL.Migrations
{
    public partial class mig_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KullaniiciId",
                table: "Sepetler");

            migrationBuilder.AddColumn<int>(
                name: "Adet",
                table: "SepetUrunler",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Sepetler",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Sepetler_AppUserId",
                table: "Sepetler",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sepetler_AspNetUsers_AppUserId",
                table: "Sepetler",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sepetler_AspNetUsers_AppUserId",
                table: "Sepetler");

            migrationBuilder.DropIndex(
                name: "IX_Sepetler_AppUserId",
                table: "Sepetler");

            migrationBuilder.DropColumn(
                name: "Adet",
                table: "SepetUrunler");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Sepetler");

            migrationBuilder.AddColumn<int>(
                name: "KullaniiciId",
                table: "Sepetler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6bc887bc-a83c-435c-9087-2394d3c20dc5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "64fbf619-d777-40cf-a825-a30957706c20");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c4dd0cf3-5ed9-4897-87e4-c045ea7c7d34");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bb7a5d25-85b9-4ad9-85f0-5db1df54504e", "AQAAAAEAACcQAAAAEBlGSF319oUa1hI4+k8VpytzEyB9hKimfWhFXo+H6UVxhK0COObHIsj5YWJgfk2+NA==" });
        }
    }
}
