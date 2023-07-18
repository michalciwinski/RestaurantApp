﻿using Microsoft.VisualStudio.Services.Users;

namespace RestaurantApp.Entities
{
    public class TOrder
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int Bill { get; set; }
        public string AdditionalComment { get; set; }
        public string State { get; set; }
        //RELATION
        public virtual TUser TUser { get; set; }
        public int TUserId { get; set; }
        public virtual TState TState { get; set; }
        public int TStateId { get; set; }




        public virtual List<TOrderPosition> TOrderPosition { get; set; }
    }
}
