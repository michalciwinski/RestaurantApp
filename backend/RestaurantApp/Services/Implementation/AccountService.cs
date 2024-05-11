using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAI;
using RestaurantApp.Authentication;
using RestaurantApp.Entities;
using RestaurantApp.Exceptions;
using RestaurantApp.Model;
using RestaurantApp.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantApp.Services.Implementation
{
    public class AccountService : ControllerBase, IAccountService
    {
        private RestaurantDbContext _context;
        private IPasswordHasher<Tuser> _passwordHasher;
        private AuthenticationSettings _authenticationSettings;
        public AccountService(RestaurantDbContext context, IPasswordHasher<Tuser> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public ActionResult RegisterUser(ModelUserRegister UserDto)
        {
            var newUser = new Tuser()
            {
                FirstName = UserDto.FirstName,
                LastName = UserDto.LastName,
                Email = UserDto.Email,
                Qr = "123",//to do - set default in DB
                Password = UserDto.Password,
                TuserTypeId = UserDto.TUserTypeId,          
            };
            var hashedPassword =_passwordHasher.HashPassword(newUser, UserDto.Password);
            newUser.Password = hashedPassword;
            _context.Tusers.Add(newUser);
            _context.SaveChanges();
            return Ok();
        }

        public string GenerateJWT(ModelUserLogin UserDto)
        {
            var user = _context.Tusers.Include(e => e.TuserType).FirstOrDefault(e => e.Email == UserDto.Email);
            if (user is null)//invalid email
            {
                throw new BadRequestException("Invalid email or password");
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, UserDto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{user.TuserType.Type}")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            //var hmac = new HMACSHA256();
            //var key = new SymmetricSecurityKey(hmac.Key);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDay);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            var jsonObject = new
            {
                Token = tokenHandler.WriteToken(token),
                Role = user.TuserType.Type
            };
            var json = JsonConvert.SerializeObject(jsonObject);
            return json;
        }


    }
}
