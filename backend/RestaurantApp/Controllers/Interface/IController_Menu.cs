using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_Menu 
    {
        IEnumerable<ModelMenu> Get();
        ModelMenu Get(int id);
        IActionResult Delete(ModelMenu Dish);
        IActionResult Post(ModelMenu Dish);
        IActionResult Put(ModelMenuToUpdate Dish);


    }
}
