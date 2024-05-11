using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_Menu 
    {
        IActionResult Get();
        IActionResult Get(int id);
        IActionResult GetIngredientsOfDish(int id);
        IActionResult Delete(int id);
        IActionResult Post(ModelMenuWithPicture modelMenuWithPicture, ModelListOfIngredients modelListOfIngredients);
        IActionResult Put(ModelMenuToUpdate Dish);


    }
}
