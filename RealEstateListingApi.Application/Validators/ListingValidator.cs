using FluentValidation;
using RealEstateListingApi.Domain.Models;

namespace RealEstateListingApi.Application.Validators;

public class ListingValidator : AbstractValidator<Listing>
{
    public ListingValidator()
    {
        RuleFor(l=>l.Title).NotEmpty().MaximumLength(50);
        RuleFor(l => l.Price).NotEmpty().PrecisionScale(10, 2, true);
        RuleFor(l => l.Description).MaximumLength(500);
    }
}
