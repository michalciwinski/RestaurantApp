using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Tmenu
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public int TdishTypeId { get; set; }

    public string SrcPict { get; set; }

    public virtual ICollection<TcompositionPosition> TcompositionPositions { get; set; } = new List<TcompositionPosition>();

    public virtual TdishType TdishType { get; set; }

    public virtual ICollection<Topinion> Topinions { get; set; } = new List<Topinion>();

    public virtual ICollection<TorderPosition> TorderPositions { get; set; } = new List<TorderPosition>();
}
