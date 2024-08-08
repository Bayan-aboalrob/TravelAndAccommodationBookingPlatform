using FluentValidation;
using TravelAndAccommodationBooking.Application.DTOs.UserDTOs.Requests;

namespace TravelAndAccommodationBooking.Application.Validators.UserValidators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6)
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[@$!%*?&#]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty().WithMessage("Confirmed Password is required.")
                .Equal(x => x.Password).WithMessage("Passwords must match.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(100).WithMessage("First Name cannot be longer than 100 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(100).WithMessage("Last Name cannot be longer than 100 characters.");

        }
    }
}
