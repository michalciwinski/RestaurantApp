using RestaurantApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.Services.Interface
{
    public interface IMenuIngredientsService
    {
        IActionResult GetDishesWithIngr();
        //ModelMenuWithIngredients GetDishWithIngr(int id);
        //public void openAiFunctions();
    }
}
