using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Tstate
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();
}
