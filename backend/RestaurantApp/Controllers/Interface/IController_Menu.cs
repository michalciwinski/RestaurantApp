using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_Menu 
    {
        IEnumerable<ModelMenu> Get();
        ModelMenu Get(int id);
        ActionResult Delete(ModelMenu Dish);
        ActionResult Post(ModelMenu Dish);
        ActionResult Put(ModelMenuToUpdate Dish);


    }
}
