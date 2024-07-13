using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Ingredient
{
    public string Ingredient_id { get; set; }

    public string Ingredient_name { get; set; }

    public decimal Stored_quantity { get; set; }

    public decimal Order_quantity { get; set; }

    public string Unit { get; set; }

    public virtual ICollection<FoodIngredient> FoodIngredients { get; set; } = new List<FoodIngredient>();
}
