using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Menu
{
    public int FoodId { get; set; } // 1
    public int Quantity { get; set; } 
    public string OrderId { get; set; }
    public DateTime TimeOrder { get; set; }
    public bool Status { get; set; } = false;
    public virtual Food Food { get; set; } 
    public virtual Orders Order { get; set; }
}
