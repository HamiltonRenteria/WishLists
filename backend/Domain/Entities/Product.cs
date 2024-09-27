using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Product : BaseEntity
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public virtual Category CategoryNavigation { get; set; } = null!;
    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
