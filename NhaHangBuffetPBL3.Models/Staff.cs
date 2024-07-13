using System;
using System.Collections.Generic;

namespace NhaHangBuffetPBL3.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? date_of_birth { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
