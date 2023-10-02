using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Model;

namespace RestaurantApp.Services.Interface
{
    public interface IAccountService
    {
        ActionResult RegisterUser(ModelUserRegister UserDto);
        void LoginUser();


    }
}
