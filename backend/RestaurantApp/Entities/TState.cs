using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Entities
{
    public class TState
    {
        [Key, Required]
        public int Id { get; set; }
        [Required,MaxLength(25)]
        public string Name { get; set; }
        //RELATION
        public virtual List<TOrder> TOrder { get; set; }
    }
}
