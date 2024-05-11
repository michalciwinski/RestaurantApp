using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class ModelUserRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TUserTypeId { get; set; } = 2;

    }
}
