using BusinessLogic.Models.Activity;
using FluentValidation;

namespace BusinessLogic.Validators;

public class ActivityValidator : AbstractValidator<ActivityDto>
{
    public ActivityValidator()
    {
        RuleFor(location => location.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(location => location.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
    }
}