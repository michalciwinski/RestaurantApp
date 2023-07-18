using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TDishType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    //RELATION
    public virtual List<TMenu> TMenu { get; set; }
}
