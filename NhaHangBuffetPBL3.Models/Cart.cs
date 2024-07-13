using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Cart
{
    public int SeaatingId { get; set; }

    public string FoodName { get; set; }

    public int FoodId { get; set; }

    public int Quantity { get; set; }
    public DateTime TimeOrder { get; set; }  
}
