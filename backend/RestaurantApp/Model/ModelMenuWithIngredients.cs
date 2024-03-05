using RestaurantApp.Entities;

namespace RestaurantApp.Model
{
    public class ModelMenuWithIngredients
    {
        public int? Id { get; set; }
        public string Name { get; set; }    
        public List <string> NameOfIngredients { get; set; }
    }
}
