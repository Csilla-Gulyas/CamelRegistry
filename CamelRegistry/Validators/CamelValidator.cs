using CamelRegistry.NewFolder;
using FluentValidation;

namespace CamelRegistry.Validators
{
    public class CamelValidator : AbstractValidator<CamelDto>
    {
        public CamelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("A név megadása kötelező.")
                .MinimumLength(3).WithMessage("A név legalább 3 karakter legyen.")
                .Matches("^[a-zA-Z]+$").WithMessage("A név csak betűket tartalmazhat.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("A szín megadása kötelező.")
                .MinimumLength(3).WithMessage("A szín legalább 3 karakter legyen.");

            RuleFor(x => x.HumpCount)
                .InclusiveBetween(1, 2).WithMessage("A púpok száma 1 vagy 2 lehet.");

            RuleFor(x => x.LastFed)
                .NotNull().WithMessage("Az etetés idejét meg kell adni.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Az etetés ideje nem lehet a jövőben.");
        }
    }
}
