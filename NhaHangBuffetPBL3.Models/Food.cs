using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Food
{
    public int FoodId { get; set; }

    public string FoodName { get; set; }

    public string Type { get; set; }

    public string? Image_Url { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();
}
