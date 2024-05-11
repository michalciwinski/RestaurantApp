using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Controllers.Implementation
{
    [Route("api/userAccount")]
    [ApiController]
    public class Controller_UserAccount : ControllerBase, IController_UserAccount
    {
        private readonly IAccountService _service;

        public Controller_UserAccount(IAccountService accountService)
        {
            _service = accountService;
        }

        [HttpPost]
        [Route("register")]
        public ActionResult RegisterUser([FromBody] ModelUserRegister User)
        {
            return _service.RegisterUser(User);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginUser([FromBody] ModelUserLogin User)
        {
            var json = _service.GenerateJWT(User);
            return Ok(json);
        }

    }
}
