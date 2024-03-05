using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TvMenuPricesDishType
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double? Price { get; set; }

    public string Dishtype { get; set; }
}
