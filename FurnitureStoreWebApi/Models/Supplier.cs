using System;
using System.Collections.Generic;

namespace FurnitureStoreWebApi.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Sname { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
