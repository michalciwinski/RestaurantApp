using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_MenuWithIngredients
    {
        IActionResult Get();
    }

}
