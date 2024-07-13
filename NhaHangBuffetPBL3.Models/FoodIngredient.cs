using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class FoodIngredient
{
    public int FoodId { get; set; }
    public string Ingredient_id { get; set; }
    public decimal Use_quantity { get; set; }
    public string Unit { get; set; }
    public virtual Food Food { get; set; }
    public virtual Ingredient Ingredient { get; set; }
}
