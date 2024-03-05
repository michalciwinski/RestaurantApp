using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Tingredient
{
    public int Id { get; set; }

    public string NameOfIngredient { get; set; }

    public virtual ICollection<TcompositionPosition> TcompositionPositions { get; set; } = new List<TcompositionPosition>();
}
