using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TdishType
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Tmenu> Tmenus { get; set; } = new List<Tmenu>();
}
