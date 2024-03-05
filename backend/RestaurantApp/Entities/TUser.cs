using System;
using System.Collections.Generic;

namespace RestaurantApp.Entities;

public partial class Tuser
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Qr { get; set; }

    public string Password { get; set; }

    public int TuserTypeId { get; set; }

    public virtual ICollection<TopinionsPosition> TopinionsPositions { get; set; } = new List<TopinionsPosition>();

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();

    public virtual TuserType TuserType { get; set; }
}
