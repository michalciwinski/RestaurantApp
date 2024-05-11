using FluentValidation;
using RestaurantApp.Entities;

namespace RestaurantApp.Model.Validators
{
    public class ModelUserLoginValidator : AbstractValidator<ModelUserLogin>
    {
        public ModelUserLoginValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
        }

    }
}
