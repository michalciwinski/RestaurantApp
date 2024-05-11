using System.Collections.Generic;

namespace RestaurantApp.Model
{
    public class ModelMenuWithPicture
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public double Price { get; set; }
            public int DishType { get; set; }
            public IFormFile ImageFile { get; set; }
    }
    
}

