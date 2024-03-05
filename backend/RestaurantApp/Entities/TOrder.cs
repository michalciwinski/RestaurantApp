using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Torder
{
    public int Id { get; set; }

    public DateTime DateOfOrder { get; set; }

    public double Bill { get; set; }

    public string AdditionalComment { get; set; }

    public int? TuserId { get; set; }

    public int? TstateId { get; set; }

    public virtual ICollection<TorderPosition> TorderPositions { get; set; } = new List<TorderPosition>();

    public virtual Tstate Tstate { get; set; }

    public virtual Tuser Tuser { get; set; }
}
