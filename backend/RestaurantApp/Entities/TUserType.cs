using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class TUserType
{
    public int Id { get; set; }
    public string Type { get; set; } = null!;
    //RELATION
    public virtual List<TUser> TUser { get; set; }
}
