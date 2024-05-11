using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Services.Interface
{
    public interface IMenuService
    {
        IActionResult GetDishes();
        IActionResult GetDish(int id);
        IActionResult GetIngredientsOfDish(int id);
        IActionResult DeleteDish(int id);
        IActionResult AddDish(ModelMenuWithPicture Dish, ModelListOfIngredients Ingredients);
        IActionResult UpdateDish(ModelMenuToUpdate Dish);
    }
}
