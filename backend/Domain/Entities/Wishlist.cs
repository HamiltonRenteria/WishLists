﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Wishlist : BaseEntity
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
