using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities;

public partial class TDishType
{
    [Key, Required]
    public int Id { get; set; }
    [Required,MaxLength(25)]
    public string Name { get; set; } = null!;
    //RELATION
    public virtual List<TMenu> TMenu { get; set; }
}
