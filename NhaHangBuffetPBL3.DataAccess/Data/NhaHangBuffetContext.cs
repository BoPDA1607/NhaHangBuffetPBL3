using System;
using System.Collections.Generic;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.DataAccess.Data;

public partial class NhaHangBuffetContext : DbContext
{
    public NhaHangBuffetContext()
    {
    }

    public NhaHangBuffetContext(DbContextOptions<NhaHangBuffetContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<FoodIngredient> FoodIngredients { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Orders> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=DESKTOP-M9VOMSI;Initial Catalog=NhaHangBuffet;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(e => e.OrderId);

        });
        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.SeatingId);
            entity.HasMany(e => e.orders)
                .WithOne(e => e.Table)
                .HasForeignKey(e => e.SeatingId);
        });
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.SeaatingId, e.FoodId });
        });
        modelBuilder.Entity<Bill>(entity =>
            {

                entity.HasKey(e => e.BillId);

                entity.HasOne(d => d.table)
                    .WithMany()
                    .HasForeignKey(d => d.SeatingId);
            });

        modelBuilder.Entity<Menu>(entity =>
        {

            entity.HasKey(e => new { e.TimeOrder, e.OrderId });

            entity.HasOne(e => e.Food)
                .WithMany(e => e.Menus)
                .HasForeignKey(e => e.FoodId);
            entity.HasOne(e => e.Order)
                .WithMany(e => e.menu)
                .HasForeignKey(e => e.OrderId);
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.FoodId);
            entity.HasMany(d => d.FoodIngredients)
                .WithOne(d => d.Food)
                .HasForeignKey(d => d.FoodId);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Ingredient_id);
        });
        modelBuilder.Entity<FoodIngredient>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.Ingredient_id });

            entity.HasOne(e => e.Ingredient)
                .WithMany(e => e.FoodIngredients)
                .HasForeignKey(e => e.Ingredient_id);


        });
        modelBuilder.Entity<Table>()
            .Property(t => t.SeatingId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Table>().HasData(
            new Table { SeatingId = 1 },
            new Table { SeatingId = 2 },
            new Table { SeatingId = 3 },
            new Table { SeatingId = 4 },
            new Table { SeatingId = 5 }
        );
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Ingredient_id = "BC", Ingredient_name = "Ba chỉ", Stored_quantity = 14.10m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "CBS", Ingredient_name = "Cá basa", Stored_quantity = 14.87m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "CC", Ingredient_name = "Cà chua", Stored_quantity = 47.00m, Order_quantity = 40.00m, Unit = "quả" },
            new Ingredient { Ingredient_id = "CCV", Ingredient_name = "Củ cải vàng", Stored_quantity = 19.90m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "CG1", Ingredient_name = "Cánh gà", Stored_quantity = 104.00m, Order_quantity = 100.00m, Unit = "cái" },
            new Ingredient { Ingredient_id = "CG2", Ingredient_name = "Chân gà", Stored_quantity = 106.00m, Order_quantity = 100.00m, Unit = "cái" },
            new Ingredient { Ingredient_id = "CORN", Ingredient_name = "Bắp mỹ", Stored_quantity = 44.00m, Order_quantity = 40.00m, Unit = "quả" },
            new Ingredient { Ingredient_id = "CT", Ingredient_name = "Cải thảo", Stored_quantity = 19.90m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "DG", Ingredient_name = "Đùi gà", Stored_quantity = 108.00m, Order_quantity = 100.00m, Unit = "cái" },
            new Ingredient { Ingredient_id = "DH", Ingredient_name = "Dưa hấu", Stored_quantity = 20.00m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "DL", Ingredient_name = "Dưa leo", Stored_quantity = 48.00m, Order_quantity = 40.00m, Unit = "quả" },
            new Ingredient { Ingredient_id = "EGG", Ingredient_name = "Trứng", Stored_quantity = 46.00m, Order_quantity = 40.00m, Unit = "quả" },
            new Ingredient { Ingredient_id = "GU", Ingredient_name = "Gói ướp", Stored_quantity = 78.00m, Order_quantity = 90.00m, Unit = "gói" },
            new Ingredient { Ingredient_id = "GV", Ingredient_name = "Gói gia vị", Stored_quantity = 93.00m, Order_quantity = 90.00m, Unit = "gói" },
            new Ingredient { Ingredient_id = "KL", Ingredient_name = "Khoai lang", Stored_quantity = 50.00m, Order_quantity = 40.00m, Unit = "củ" },
            new Ingredient { Ingredient_id = "KT", Ingredient_name = "Khoai tây", Stored_quantity = 50.00m, Order_quantity = 40.00m, Unit = "củ" },
            new Ingredient { Ingredient_id = "OI", Ingredient_name = "Ổi", Stored_quantity = 7.00m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "RB", Ingredient_name = "Rong biển", Stored_quantity = 54.00m, Order_quantity = 50.00m, Unit = "lát" },
            new Ingredient { Ingredient_id = "RICE", Ingredient_name = "Cơm", Stored_quantity = 19.40m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "SL", Ingredient_name = "Salad", Stored_quantity = 29.18m, Order_quantity = 20.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "SS", Ingredient_name = "Xúc xích", Stored_quantity = 49.00m, Order_quantity = 40.00m, Unit = "cây" },
            new Ingredient { Ingredient_id = "TB", Ingredient_name = "Thăn bò", Stored_quantity = 14.40m, Order_quantity = 10.00m, Unit = "kg" },
            new Ingredient { Ingredient_id = "TOK", Ingredient_name = "Tteokbokki", Stored_quantity = 29.70m, Order_quantity = 20.00m, Unit = "kg" }
        );
        modelBuilder.Entity<FoodIngredient>().HasData(
            new FoodIngredient { FoodId = 1, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 1, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 1, Ingredient_id = "TB", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 2, Ingredient_id = "BC", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 2, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 2, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 3, Ingredient_id = "CG1", Use_quantity = 2.00m, Unit = "cái" },
            new FoodIngredient { FoodId = 3, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 3, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 4, Ingredient_id = "CG2", Use_quantity = 2.00m, Unit = "cái" },
            new FoodIngredient { FoodId = 4, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 4, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 5, Ingredient_id = "DG", Use_quantity = 2.00m, Unit = "cái" },
            new FoodIngredient { FoodId = 5, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 5, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 6, Ingredient_id = "CBS", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 6, Ingredient_id = "GU", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 6, Ingredient_id = "SL", Use_quantity = 0.01m, Unit = "kg" },
            new FoodIngredient { FoodId = 7, Ingredient_id = "CC", Use_quantity = 1.00m, Unit = "quả" },
            new FoodIngredient { FoodId = 7, Ingredient_id = "DL", Use_quantity = 0.50m, Unit = "quả" },
            new FoodIngredient { FoodId = 7, Ingredient_id = "EGG", Use_quantity = 1.00m, Unit = "quả" },
            new FoodIngredient { FoodId = 7, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 7, Ingredient_id = "SL", Use_quantity = 0.20m, Unit = "kg" },
            new FoodIngredient { FoodId = 8, Ingredient_id = "CORN", Use_quantity = 1.00m, Unit = "quả" },
            new FoodIngredient { FoodId = 9, Ingredient_id = "CCV", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 9, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 10, Ingredient_id = "CT", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 10, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 11, Ingredient_id = "CBS", Use_quantity = 0.03m, Unit = "kg" },
            new FoodIngredient { FoodId = 11, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 11, Ingredient_id = "RB", Use_quantity = 1.00m, Unit = "lát" },
            new FoodIngredient { FoodId = 11, Ingredient_id = "RICE", Use_quantity = 0.10m, Unit = "kg" },
            new FoodIngredient { FoodId = 12, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 12, Ingredient_id = "TOK", Use_quantity = 0.30m, Unit = "kg" },
            new FoodIngredient { FoodId = 13, Ingredient_id = "DL", Use_quantity = 0.50m, Unit = "quả" },
            new FoodIngredient { FoodId = 13, Ingredient_id = "EGG", Use_quantity = 1.00m, Unit = "quả" },
            new FoodIngredient { FoodId = 13, Ingredient_id = "RB", Use_quantity = 5.00m, Unit = "lát" },
            new FoodIngredient { FoodId = 13, Ingredient_id = "RICE", Use_quantity = 0.50m, Unit = "kg" },
            new FoodIngredient { FoodId = 13, Ingredient_id = "SS", Use_quantity = 1.00m, Unit = "cây" },
            new FoodIngredient { FoodId = 14, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 14, Ingredient_id = "KT", Use_quantity = 1.00m, Unit = "củ" },
            new FoodIngredient { FoodId = 15, Ingredient_id = "GV", Use_quantity = 1.00m, Unit = "gói" },
            new FoodIngredient { FoodId = 15, Ingredient_id = "KL", Use_quantity = 1.00m, Unit = "củ" },
            new FoodIngredient { FoodId = 16, Ingredient_id = "DH", Use_quantity = 0.50m, Unit = "kg" },
            new FoodIngredient { FoodId = 17, Ingredient_id = "OI", Use_quantity = 1.00m, Unit = "quả" }
        );
        modelBuilder.Entity<Food>().HasData(
            new Food { FoodId = 1, FoodName = "Thăn bò nướng", Type = "meat", Image_Url = "https://www.slenderkitchen.com/sites/default/files/styles/gsd-1x1/public/recipe_images/grilled-sirloin-chimichurri.jpg" },
            new Food { FoodId = 2, FoodName = "Ba chỉ nướng", Type = "meat", Image_Url = "https://chedoan.com/wp-content/uploads/2023/04/cach-uop-thit-ba-chi-nuong-4.jpg" },
            new Food { FoodId = 3, FoodName = "Cánh gà nướng", Type = "meat", Image_Url = "https://poshjournal.com/wp-content/uploads/2021/02/thai-bbq-chicken-wings-8.jpg" },
            new Food { FoodId = 4, FoodName = "Chân gà nướng", Type = "meat", Image_Url = "https://nghebep.com/wp-content/uploads/2018/07/chan-ga-nuong-mat-ong.jpg" },
            new Food { FoodId = 5, FoodName = "Đùi gà nướng", Type = "meat", Image_Url = "https://cdn.tgdd.vn/2020/06/CookProduct/21-1200x676-1.jpg" },
            new Food { FoodId = 6, FoodName = "Cá basa nướng", Type = "meat", Image_Url = "https://www.licious.in/blog/wp-content/uploads/2020/12/Grilled-Fish.jpg" },
            new Food { FoodId = 7, FoodName = "Salad trộn", Type = "vegetable", Image_Url = "https://www.zimbokitchen.com/wp-content/uploads/2020/07/GREEN-SALAD-II.jpg" },
            new Food { FoodId = 8, FoodName = "Bắp mỹ", Type = "vegetable", Image_Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRmEH8Xu8lA_wB4SkdUTDARrjo0SqNJOQAKxVtc7youqw&s" },
            new Food { FoodId = 9, FoodName = "Củ cải vàng muối", Type = "vegetable", Image_Url = "https://wikianngon.com/wp-content/uploads/2020/09/cu-cai-vang-muoi-1.jpg" },
            new Food { FoodId = 10, FoodName = "Kim chi", Type = "vegetable", Image_Url = "https://www.huongnghiepaau.com/wp-content/uploads/2022/06/kim-chi-cai-thao-chua-vi-han-quoc.jpg" },
            new Food { FoodId = 11, FoodName = "Onigiri", Type = "rice", Image_Url = "https://www.onigiri.casa/static/8fd936b83d13a9eed5b0cbe6346beccd/5d415/onigiri-salmon.jpg" },
            new Food { FoodId = 12, FoodName = "Tteokbokki", Type = "rice", Image_Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4d/Tteokbokki.JPG/640px-Tteokbokki.JPG" },
            new Food { FoodId = 13, FoodName = "Kimbap", Type = "rice", Image_Url = "https://nhatkycuacoay.com/wp-content/uploads/2022/05/huong-dan-lam-com-cuon-rong-bien7.webp" },
            new Food { FoodId = 14, FoodName = "Khoai tây chiên", Type = "snack", Image_Url = "https://www.recipetineats.com/wp-content/uploads/2022/09/Fries-with-rosemary-salt_1.jpg" },
            new Food { FoodId = 15, FoodName = "Khoai lang lắc", Type = "snack", Image_Url = "https://www.ilovelindsay.com/assets/components/phpthumbof/cache/yukonsweetpotatoes_small.f82724c758f5a7e1142cf0568b85d580.jpg" },
            new Food { FoodId = 16, FoodName = "Dưa hấu", Type = "dessert", Image_Url = "https://hips.hearstapps.com/hmg-prod/images/fresh-ripe-watermelon-slices-on-wooden-table-royalty-free-image-1684966820.jpg?crop=0.88973xw:1xh;center,top&resize=1200:*" },
            new Food { FoodId = 17, FoodName = "Ổi", Type = "dessert", Image_Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQnacAyYre-T-SiLPfe3zc_XQPuymPU2ZNpJjTAiUTXSA&s" }
        );
        modelBuilder.Entity<Staff>().HasData(
            new Staff { Id = 1, Username = "Admin", Password = BCrypt.Net.BCrypt.HashPassword("Admin") },
            new Staff { Id = 2, Username = "Anh", Password = BCrypt.Net.BCrypt.HashPassword("Anh123") },
            new Staff { Id = 3, Username = "Bac", Password = BCrypt.Net.BCrypt.HashPassword("Bac123") },
            new Staff { Id = 4, Username = "Nhi", Password = BCrypt.Net.BCrypt.HashPassword("Nhi123") }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
