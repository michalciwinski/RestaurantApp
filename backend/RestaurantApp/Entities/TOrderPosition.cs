﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public partial class TOrderPosition
    {
        [Key, Required]
        public int Id { get; set; }
        //RELATION
        public virtual TMenu TMenu { get; set; }
        public int TMenuId { get; set; }
        public virtual TOrder TOrder { get; set; }
        public int TOrderId { get; set; }


    }
}
