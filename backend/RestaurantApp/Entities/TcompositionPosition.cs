using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TcompositionPosition
{
    public int Id { get; set; }

    public int TmenuId { get; set; }

    public int TingredientsId { get; set; }

    public virtual Tingredient Tingredients { get; set; }

    public virtual Tmenu Tmenu { get; set; }
}
