using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TMenu
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double Price { get; set; }
    //RELATION
    public virtual TDishType TDishType { get; set; }
    public int TDishTypeId { get; set; }
    public virtual List<TOrderPosition> TOrderPosition { get; set; } //relation - many

}
