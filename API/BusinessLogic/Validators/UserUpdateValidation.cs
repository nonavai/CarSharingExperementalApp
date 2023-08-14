using BusinessLogic.Models.User;
using FluentValidation;

namespace BusinessLogic.Validators;

public class UserUpdateValidation : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidation()
    {
        RuleFor(user => user.FirstName)
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(user => user.LastName)
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(user => user.Email)
            .EmailAddress().WithMessage("Please provide a valid email address.")
            .MaximumLength(100).WithMessage("Email address cannot exceed 100 characters.");

        RuleFor(user => user.Password)
            .MinimumLength(6).WithMessage("Password must be at least 8 characters long.");

        RuleFor(user => user.PhoneNumber)
            .Matches(@"^[0-9]+$").WithMessage("Phone number must contain only digits.");

        RuleFor(user => user.RecordNumber)
            .MaximumLength(20).WithMessage("Record number cannot exceed 20 characters.");

        RuleFor(user => user.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
    
}