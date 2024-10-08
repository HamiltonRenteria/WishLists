﻿using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool State { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
