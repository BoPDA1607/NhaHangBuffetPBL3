using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Orders
{
    public string OrderId { get; set; }
    public int SeatingId { get; set; }
    public int IsUsed { get; set; }
    public DateTime? SeatingDate { get; set; }
    public virtual Table Table { get; set; }
    public virtual ICollection<Menu>  menu { get; set; }=new List<Menu>();
}
