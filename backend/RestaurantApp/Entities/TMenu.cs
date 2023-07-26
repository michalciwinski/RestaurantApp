using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities;

public partial class TMenu
{
    [Key, Required]
    public int Id { get; set; }
    [Required, MaxLength(25)]
    public string Name { get; set; }
    [Required, MaxLength(50)]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    //RELATION
    public virtual TDishType TDishType { get; set; }
    public int TDishTypeId { get; set; }
    public virtual List<TOrderPosition> TOrderPosition { get; set; } 

}
