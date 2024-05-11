using FluentValidation;
using RestaurantApp.Entities;

namespace RestaurantApp.Model.Validators
{
    public class ModelUserRegisterValidator : AbstractValidator<ModelUserRegister>
    {
        public ModelUserRegisterValidator(RestaurantDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailOccupied = dbContext.Tusers.Any(x => x.Email == value);
                if (emailOccupied)
                {
                    context.AddFailure("Email", "Email is taken");
                }
            });
        }

    }
}
