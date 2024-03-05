using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Entities;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;

namespace RestaurantApp.Services.Implementation
{
    public class AccountService : ControllerBase, IAccountService
    {
        private RestaurantDbContext _context;
        public AccountService(RestaurantDbContext context)
        {
            _context = context;
        }

        public ActionResult RegisterUser(ModelUserRegister UserDto)
        {
            var newUser = new Tuser()
            {
                FirstName = UserDto.FirstName,
                LastName = UserDto.LastName,
                Email = UserDto.Email,
                Qr = "123",//to do
                Password = UserDto.Password,
                TuserTypeId = UserDto.TUserTypeId,          
            };
            try
            {
                _context.Tusers.Add(newUser);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                  return BadRequest("error with adding user to database"); 
            }
        }

        public void LoginUser()
        {


        }


    }
}
