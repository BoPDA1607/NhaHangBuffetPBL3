using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NhaHangBuffetPBL3.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Down(migrationBuilder);
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    SeaatingId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TimeOrder = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => new { x.SeaatingId, x.FoodId });
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image_Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Ingredient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ingredient_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stored_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Order_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Ingredient_id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    SeatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.SeatingId);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredients",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Ingredient_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Use_quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredients", x => new { x.FoodId, x.Ingredient_id });
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIngredients_Ingredients_Ingredient_id",
                        column: x => x.Ingredient_id,
                        principalTable: "Ingredients",
                        principalColumn: "Ingredient_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SeatingId = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bills_Tables_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Tables",
                        principalColumn: "SeatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SeatingId = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<int>(type: "int", nullable: false),
                    SeatingDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_SeatingId",
                        column: x => x.SeatingId,
                        principalTable: "Tables",
                        principalColumn: "SeatingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => new { x.TimeOrder, x.OrderId });
                    table.ForeignKey(
                        name: "FK_Menus_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "FoodName", "Image_Url", "Type" },
                values: new object[,]
                {
                    { 1, "Thăn bò nướng", "https://www.slenderkitchen.com/sites/default/files/styles/gsd-1x1/public/recipe_images/grilled-sirloin-chimichurri.jpg", "meat" },
                    { 2, "Ba chỉ nướng", "https://chedoan.com/wp-content/uploads/2023/04/cach-uop-thit-ba-chi-nuong-4.jpg", "meat" },
                    { 3, "Cánh gà nướng", "https://poshjournal.com/wp-content/uploads/2021/02/thai-bbq-chicken-wings-8.jpg", "meat" },
                    { 4, "Chân gà nướng", "https://nghebep.com/wp-content/uploads/2018/07/chan-ga-nuong-mat-ong.jpg", "meat" },
                    { 5, "Đùi gà nướng", "https://lemonsandzest.com/wp-content/uploads/2022/05/Grilled-Chicken-Legs-8.jpg", "meat" },
                    { 6, "Cá basa nướng", "https://www.licious.in/blog/wp-content/uploads/2020/12/Grilled-Fish.jpg", "meat" },
                    { 7, "Salad trộn", "https://www.zimbokitchen.com/wp-content/uploads/2020/07/GREEN-SALAD-II.jpg", "vegetable" },
                    { 8, "Bắp mỹ", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmEH8Xu8lA_wB4SkdUTDARrjo0SqNJOQAKxVtc7youqw&s", "vegetable" },
                    { 9, "Củ cải vàng muối", "https://wikianngon.com/wp-content/uploads/2020/09/cu-cai-vang-muoi-1.jpg", "vegetable" },
                    { 10, "Kim chi", "https://www.huongnghiepaau.com/wp-content/uploads/2022/06/kim-chi-cai-thao-chua-vi-han-quoc.jpg", "vegetable" },
                    { 11, "Onigiri", "https://www.onigiri.casa/static/8fd936b83d13a9eed5b0cbe6346beccd/5d415/onigiri-salmon.jpg", "rice" },
                    { 12, "Tteokbokki", "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/Tteokbokki.JPG/640px-Tteokbokki.JPG", "rice" },
                    { 13, "Kimbap", "https://nhatkycuacoay.com/wp-content/uploads/2022/05/huong-dan-lam-com-cuon-rong-bien7.webp", "rice" },
                    { 14, "Khoai tây chiên", "https://www.recipetineats.com/wp-content/uploads/2022/09/Fries-with-rosemary-salt_1.jpg", "snack" },
                    { 15, "Khoai lang lắc", "https://www.ilovelindsay.com/assets/components/phpthumbof/cache/yukonsweetpotatoes_small.f82724c758f5a7e1142cf0568b85d580.jpg", "snack" },
                    { 16, "Dưa hấu", "https://hips.hearstapps.com/hmg-prod/images/fresh-ripe-watermelon-slices-on-wooden-table-royalty-free-image-1684966820.jpg?crop=0.88973xw:1xh;center,top&resize=1200:*", "dessert" },
                    { 17, "Ổi", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQnacAyYre-T-SiLPfe3zc_XQPuymPU2ZNpJjTAiUTXSA&s", "dessert" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Ingredient_id", "Ingredient_name", "Order_quantity", "Stored_quantity", "Unit" },
                values: new object[,]
                {
                    { "BC", "Ba chỉ", 10.00m, 14.10m, "kg" },
                    { "CBS", "Cá basa", 10.00m, 14.87m, "kg" },
                    { "CC", "Cà chua", 40.00m, 47.00m, "quả" },
                    { "CCV", "Củ cải vàng", 10.00m, 19.90m, "kg" },
                    { "CG1", "Cánh gà", 100.00m, 104.00m, "cái" },
                    { "CG2", "Chân gà", 100.00m, 106.00m, "cái" },
                    { "CORN", "Bắp mỹ", 40.00m, 44.00m, "quả" },
                    { "CT", "Cải thảo", 10.00m, 19.90m, "kg" },
                    { "DG", "Đùi gà", 100.00m, 108.00m, "cái" },
                    { "DH", "Dưa hấu", 10.00m, 20.00m, "kg" },
                    { "DL", "Dưa leo", 40.00m, 48.00m, "quả" },
                    { "EGG", "Trứng", 40.00m, 46.00m, "quả" },
                    { "GU", "Gói ướp", 90.00m, 78.00m, "gói" },
                    { "GV", "Gói gia vị", 90.00m, 93.00m, "gói" },
                    { "KL", "Khoai lang", 40.00m, 50.00m, "củ" },
                    { "KT", "Khoai tây", 40.00m, 50.00m, "củ" },
                    { "OI", "Ổi", 10.00m, 7.00m, "kg" },
                    { "RB", "Rong biển", 50.00m, 54.00m, "lát" },
                    { "RICE", "Cơm", 10.00m, 19.40m, "kg" },
                    { "SL", "Salad", 20.00m, 29.18m, "kg" },
                    { "SS", "Xúc xích", 40.00m, 49.00m, "cây" },
                    { "TB", "Thăn bò", 10.00m, 14.40m, "kg" },
                    { "TOK", "Tteokbokki", 20.00m, 29.70m, "kg" }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Name", "Password", "Username", "date_of_birth" },
                values: new object[,]
                {
                    { 1, null, "admin", "admin", null },
                    { 2, null, "Anh123", "Anh", null },
                    { 3, null, "Bac123", "Bac", null },
                    { 4, null, "Nhi123", "Nhi", null }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "SeatingId", "NumberOfPeople", "Status" },
                values: new object[,]
                {
                    { 1, null, "waiting" },
                    { 2, null, "waiting" },
                    { 3, null, "waiting" },
                    { 4, null, "waiting" },
                    { 5, null, "waiting" }
                });

            migrationBuilder.InsertData(
                table: "FoodIngredients",
                columns: new[] { "FoodId", "Ingredient_id", "Unit", "Use_quantity" },
                values: new object[,]
                {
                    { 1, "GU", "gói", 1.00m },
                    { 1, "SL", "kg", 0.01m },
                    { 1, "TB", "kg", 0.10m },
                    { 2, "BC", "kg", 0.10m },
                    { 2, "GU", "gói", 1.00m },
                    { 2, "SL", "kg", 0.01m },
                    { 3, "CG1", "cái", 2.00m },
                    { 3, "GU", "gói", 1.00m },
                    { 3, "SL", "kg", 0.01m },
                    { 4, "CG2", "cái", 2.00m },
                    { 4, "GU", "gói", 1.00m },
                    { 4, "SL", "kg", 0.01m },
                    { 5, "DG", "cái", 2.00m },
                    { 5, "GU", "gói", 1.00m },
                    { 5, "SL", "kg", 0.01m },
                    { 6, "CBS", "kg", 0.10m },
                    { 6, "GU", "gói", 1.00m },
                    { 6, "SL", "kg", 0.01m },
                    { 7, "CC", "quả", 1.00m },
                    { 7, "DL", "quả", 0.50m },
                    { 7, "EGG", "quả", 1.00m },
                    { 7, "GV", "gói", 1.00m },
                    { 7, "SL", "kg", 0.20m },
                    { 8, "CORN", "quả", 1.00m },
                    { 9, "CCV", "kg", 0.10m },
                    { 9, "GV", "gói", 1.00m },
                    { 10, "CT", "kg", 0.10m },
                    { 10, "GV", "gói", 1.00m },
                    { 11, "CBS", "kg", 0.03m },
                    { 11, "GV", "gói", 1.00m },
                    { 11, "RB", "lát", 1.00m },
                    { 11, "RICE", "kg", 0.10m },
                    { 12, "GV", "gói", 1.00m },
                    { 12, "TOK", "kg", 0.30m },
                    { 13, "DL", "quả", 0.50m },
                    { 13, "EGG", "quả", 1.00m },
                    { 13, "RB", "lát", 5.00m },
                    { 13, "RICE", "kg", 0.50m },
                    { 13, "SS", "cây", 1.00m },
                    { 14, "GV", "gói", 1.00m },
                    { 14, "KT", "củ", 1.00m },
                    { 15, "GV", "gói", 1.00m },
                    { 15, "KL", "củ", 1.00m },
                    { 16, "DH", "kg", 0.50m },
                    { 17, "OI", "quả", 1.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_SeatingId",
                table: "Bills",
                column: "SeatingId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredients_Ingredient_id",
                table: "FoodIngredients",
                column: "Ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FoodId",
                table: "Menus",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_OrderId",
                table: "Menus",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SeatingId",
                table: "Orders",
                column: "SeatingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "FoodIngredients");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
