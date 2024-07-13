using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public DateTime BillDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int SeatingId { get; set; }

    public string PaymentType { get; set; } = null!;

    public int NumberOfPeople { get; set; }
    public virtual Table table { get; set; }
}

