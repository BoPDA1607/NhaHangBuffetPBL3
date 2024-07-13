using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NhaHangBuffetPBL3.Models;

public partial class Table
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SeatingId { get; set; }
   
    public int? NumberOfPeople { get; set; }
    public string Status { get; set; } = "waiting";
    public virtual ICollection<Orders> orders { get; set; } = new List<Orders>();
}
