using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FurnitureStoreWebApi.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Password { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
