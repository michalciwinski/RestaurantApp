using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestaurantApp.Model;

namespace RestaurantApp.Services.Interface
{
    public interface IAccountService
    {
        ActionResult RegisterUser(ModelUserRegister UserDto);
        string GenerateJWT(ModelUserLogin user);
    }
}
