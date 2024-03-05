using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TuserType
{
    public int Id { get; set; }

    public string Type { get; set; }

    public virtual ICollection<Tuser> Tusers { get; set; } = new List<Tuser>();
}
