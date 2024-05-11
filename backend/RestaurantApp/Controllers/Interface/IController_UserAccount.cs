using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_UserAccount
    {
        ActionResult RegisterUser(ModelUserRegister User);
        ActionResult LoginUser(ModelUserLogin User);
    }
}
