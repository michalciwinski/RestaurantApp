using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities;

public partial class TUserType
{
    [Key, Required]
    public int Id { get; set; }
    [Required, MaxLength(25)]
    public string Type { get; set; } 
    //RELATION
    public virtual List<TUser> TUser { get; set; }
}
