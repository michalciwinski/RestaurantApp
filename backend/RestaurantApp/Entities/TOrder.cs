using Microsoft.VisualStudio.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public partial class TOrder
    {
        [Key, Required]
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        [Required, MaxLength(25)]
        public double Bill { get; set; }
        public string AdditionalComment { get; set; }
        //RELATION
        public int? TUserId { get; set; }
        public virtual TUser TUser { get; set; }
        public int? TStateId { get; set; }
        public virtual TState TState { get; set; }


        public virtual List<TOrderPosition> TOrderPosition { get; set; }
    }
}
