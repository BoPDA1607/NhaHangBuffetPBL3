using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NhaHangBuffetPBL3.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: 5,
                column: "Image_Url",
                value: "https://cdn.tgdd.vn/2020/06/CookProduct/21-1200x676-1.jpg");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "$2a$11$9bwwLknSgYs9cewDLBVG8elnYy0hA65XHtYq4UVa4lg0Rj.n3q3ca", "Admin" });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$ZdISB2ErtIuA5eh6sYezCeaDWAwKQ7xSFB7T5P.w9limtGMdNu3bq");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$GoQhXWLADMErQhxQgsnq8uBs/g7AL3/K0y6pEcpMZ.U.GGlAgIB7u");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$YBvQ/orY5j/pWSyeHlqmSON6BWjjVRLSQUr6MIjjtSIUxBEPUZYeW");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Foods",
                keyColumn: "FoodId",
                keyValue: 5,
                column: "Image_Url",
                value: "https://lemonsandzest.com/wp-content/uploads/2022/05/Grilled-Chicken-Legs-8.jpg");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "admin", "admin" });

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "Anh123");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "Bac123");

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "Nhi123");
        }
    }
}
