using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Topinion
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public int TmenuId { get; set; }

    public virtual Tmenu Tmenu { get; set; }

    public virtual ICollection<TopinionsPosition> TopinionsPositions { get; set; } = new List<TopinionsPosition>();
}
