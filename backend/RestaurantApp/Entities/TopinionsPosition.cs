using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TopinionsPosition
{
    public int Id { get; set; }

    public int TopinionsId { get; set; }

    public int? Tuser { get; set; }

    public DateOnly Date { get; set; }

    public virtual Topinion Topinions { get; set; }

    public virtual Tuser TuserNavigation { get; set; }
}
