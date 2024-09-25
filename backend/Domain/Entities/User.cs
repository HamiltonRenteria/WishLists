using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
