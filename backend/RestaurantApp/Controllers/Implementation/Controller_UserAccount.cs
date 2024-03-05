using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Controllers.Interface;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Controllers.Implementation
{
    [Route("[controller]")]
    [ApiController]
    public class Controller_UserAccount : ControllerBase, IController_UserAccount
    {
        private readonly IAccountService _service;
        //private readonly RestaurantDbContext _DB;

        public Controller_UserAccount(IAccountService accountService)
        {
            //_DB = new RestaurantDbContext();
            _service = accountService;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public ActionResult Post([FromBody] ModelUserRegister User)
        {
            return _service.RegisterUser(User);
        }

    }
}
