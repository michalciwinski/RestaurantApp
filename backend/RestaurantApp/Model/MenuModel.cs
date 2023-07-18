using RestaurantApp.Entities;

namespace RestaurantApp.Model
{
    //MAPPING TABLE DISH TO MODEL
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public double Price { get; set; }
        //public DishTypeModel DishType { get; set; } 


    }
}
