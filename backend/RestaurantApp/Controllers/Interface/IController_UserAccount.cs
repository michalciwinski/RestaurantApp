using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Controllers.Interface
{
    public interface IController_UserAccount
    {
        ActionResult Post(ModelUserRegister User);

    }
}
