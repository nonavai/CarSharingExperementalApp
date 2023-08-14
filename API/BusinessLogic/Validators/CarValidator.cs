using BusinessLogic.Models.Car;
using FluentValidation;

namespace BusinessLogic.Validators;

public class CarValidator : AbstractValidator<CarDto>
{
    public CarValidator()
    {
        RuleFor(car => car.Year)
            .InclusiveBetween(1900, DateTime.UtcNow.Year)
            .WithMessage("Year must be between 1900 and the current year.");

        RuleFor(car => car.RegistrationNumber)
            .NotEmpty().WithMessage("Registration number is required.")
            .Matches(@"^[A-Za-z0-9]+$").WithMessage("Registration number must contain only letters and numbers.");

        RuleFor(car => car.Mark)
            .NotEmpty().WithMessage("Car mark is required.")
            .MaximumLength(30).WithMessage("Car mark cannot exceed 30 characters.");

        RuleFor(car => car.Model)
            .NotEmpty().WithMessage("Car model is required.")
            .MaximumLength(30).WithMessage("Car model cannot exceed 30 characters.");

        RuleFor(car => car.VehicleBody)
            .NotEmpty()
            .MinimumLength(9).WithMessage("VehicleBody num must be longer than 9.")
            .MaximumLength(17).WithMessage("VehicleBody cannot exceed 12 characters.");

        RuleFor(car => car.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
    }

}