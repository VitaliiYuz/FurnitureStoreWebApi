using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace FurnitureStoreWebApi.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public double Price { get; set; }

    public int StockQuantity { get; set; }

    public int SupplierId { get; set; }

    public virtual Category Category { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    [JsonIgnore]
    public virtual Supplier Supplier { get; set; } = null!;
}
