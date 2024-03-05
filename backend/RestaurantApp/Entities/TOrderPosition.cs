using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TorderPosition
{
    public int Id { get; set; }

    public int TmenuId { get; set; }

    public int TorderId { get; set; }

    public virtual Tmenu Tmenu { get; set; }

    public virtual Torder Torder { get; set; }
}
